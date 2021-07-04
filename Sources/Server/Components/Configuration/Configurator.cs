using System;
using System.Configuration;
using Db4objects.Db4o;
using Db4objects.Db4o.CS.Config;
using Popupnik.Common.Utils.Logging;
using Popupnik.Server.Components.DataAccess;
using Popupnik.Server.Components.DataAccessBase;

namespace Popupnik.Server.Components.Configuration
{
    //TODO: Wrap all in try/catch
    //TODO: Inherit from ComponentBase and add Init, Deinit methods
    //TODO: Split by components -> DbConfigurator, LoggerConfigurator, ...
    public static class Configurator
    {
        #region Fields

        private static IDatabaseManager databaseManager;
        private static IDatabaseContextProvider databaseContextProvider;
        private static IDatabaseServerManager<IObjectServer, IServerConfiguration, IClientConfiguration> databaseServerManager;
        private static SettingsCollection collection;

        #endregion

        #region Properties

        public static bool IsInitialized
        {
            get
            {
                return true &&
                       Logger.IsInitialized &&
                       databaseManager != null &&
                       databaseContextProvider != null;
            }
        }

        public static IDatabaseManager DatabaseManager
        {
            get
            {
                if (databaseManager == null)
                {
                    
                }
                return databaseManager;
            }
        }

        public static IDatabaseContextProvider DatabaseContextProvider
        {
            get { return databaseContextProvider; }
        }

        #endregion

        //TODO: Move to SettingsHelper?
        public static T MapProperties<T>(SettingsBase settings)
        {
            var instance = (T) Activator.CreateInstance(typeof (T));
            foreach (var property in typeof(T).GetProperties())
            {
                object[] attributes = property.GetCustomAttributes(
                    typeof(PropertyMappingAttribute), false);

                object value;
                if (attributes.Length == 1)
                {
                    var propertyMappingAttribute = (PropertyMappingAttribute)attributes[0];
                    value = settings[propertyMappingAttribute.Name];
                }
                else
                {
                    value = settings[property.Name];
                }
                property.SetValue(instance, value, null);
            }
            return instance;
        }

        #region Init/Deinit components

        public static void InitializeComponents(SettingsCollection settingsCollection)
        {
            collection = settingsCollection;
            InitializeLogger();
            InitializeDatabase();
        }

        public static void DeinitializeComponents()
        {
            DeinitializeDatabase();
            DeinitializeLogger();
        }

        #region Init/Deinit Logger

        private static void InitializeLogger()
        {
            Logger.Initialize();
        }

        private static void DeinitializeLogger()
        {
            Logger.Deinitialize();
        }

        #endregion

        #region Init/Deinit database entities

        private static void InitializeDatabase()
        {
            IDatabaseSettings settings = MapProperties<DatabaseSettings>(collection.Database);
            databaseServerManager = new Db4ODatabaseServerManager(settings);
            databaseManager = new Db4ODatabaseManager(settings);
            databaseContextProvider = new Db4ODatabaseContextProvider(
                settings,
                databaseServerManager
                );
        }

        private static void DeinitializeDatabase()
        {
            if (databaseManager != null)
            {
                databaseManager.Dispose();
            }
            if (databaseContextProvider != null)
            {
                databaseContextProvider.Dispose();
            }
            if (databaseServerManager != null)
            {
                databaseServerManager.Dispose();
            }
        }

        #endregion

        #endregion
    }
}