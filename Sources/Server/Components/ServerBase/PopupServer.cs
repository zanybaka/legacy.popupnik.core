using System;
using Popupnik.Server.Components.Configuration;

namespace Popupnik.Server.Components.ServerBase
{
    public sealed class PopupServer
    {
        private IServerSettings Settings { get; set; }

        public PopupServer(IServerSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            Settings = settings;
        }

        public void Run()
        {
            if (!Configurator.DatabaseManager.IsDbExists())
            {
                Configurator.DatabaseManager.CreateDb();
            }
        }

        public void Shutdown()
        {
            
        }
    }
}