using System;
using System.Reflection;
using Popupnik.Common.Utils.Logging;
using Popupnik.Server.Applications.Console.Properties;
using Popupnik.Server.Components.Configuration;
using Popupnik.Server.Components.ServerBase;

namespace Popupnik.Server.Applications.Console
{
    //TODO: Create resources.resx
    internal class Program
    {
        private static PopupServer server;

        private static void Main( /*string[] args*/)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            
            ShowHeader();

            InitializeComponents();

            InitializeServer();

            try
            {
                StartServer();

                ShutdownServer();
            }
            finally
            {
                DeinitializeComponents();
            }

            ShowFooter();
        }

        private static void ShutdownServer()
        {
            server.Shutdown();
            System.Console.WriteLine("Server has been stopped succesfully.");
        }

        private static void StartServer()
        {
            server.Run();
            System.Console.WriteLine("Server has been started succesfully.");

            System.Console.WriteLine("Press Enter to stop...");
            System.Console.ReadLine();
        }

        private static void InitializeServer()
        {
            var serverSettings = Configurator.MapProperties<ConsoleSettings>(Settings.Default);
            server = new PopupServer(serverSettings);
            System.Console.WriteLine("Server instance has been created succesfully.");
        }

        private static void DeinitializeComponents()
        {
            Configurator.DeinitializeComponents();
            System.Console.WriteLine("Components has been deinitialized succesfully.");
        }

        private static void InitializeComponents()
        {
            var componentSettings = new SettingsCollection
                                        {
                                            Database = Settings.Default,
                                            Logger = Settings.Default
                                        };

            Configurator.InitializeComponents(componentSettings);
            System.Console.WriteLine("Components has been initialized succesfully.");
        }

        private static void ShowFooter()
        {
            System.Console.WriteLine("Popupnik Server Console has been finished.");
        }

        private static void ShowHeader()
        {
            System.Console.WriteLine("Popupnik Server Console v.{0}, ©  2009.", Assembly.GetCallingAssembly().GetName().Version);
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.WriteLineFormat("Unhandled exception: {0}", e.ExceptionObject);
            /*
            if (e.ExceptionObject is FooException)
            {
                Logger.WriteLine("This usually means, that ... ! Current exception is being suppressed.");
                return;
            }
            */
            Logger.WriteLine("Cannot recover from this error. Going to quit now...");
            Configurator.DeinitializeComponents();
            Environment.Exit(-1);
        }
    }
}