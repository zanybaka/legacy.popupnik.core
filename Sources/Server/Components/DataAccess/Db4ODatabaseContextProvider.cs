using System;
using Db4objects.Db4o;
using Db4objects.Db4o.CS.Config;
using Popupnik.Server.Components.DataAccessBase;

namespace Popupnik.Server.Components.DataAccess
{
    //TODO: Create Local and Remote db4o provider
    public sealed class Db4ODatabaseContextProvider : IDatabaseContextProvider
    {
        private IDatabaseServerManager<IObjectServer, IServerConfiguration, IClientConfiguration> serverManager;
        private IDatabaseSettings settings;

        public Db4ODatabaseContextProvider(
            IDatabaseSettings settings,
            IDatabaseServerManager<IObjectServer, IServerConfiguration, IClientConfiguration> serverManager
            )
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            if (serverManager == null)
            {
                throw new ArgumentNullException("serverManager");
            }
            this.serverManager = serverManager;
            this.settings = settings;
        }

        #region Implementation of IDatabaseContextProvider

        public IDatabaseContext CreateContext()
        {
            switch (serverManager.Type)
            {
                case ServerType.Local:
                    return new Db4OLocalDatabaseContext(serverManager.GetServer);
                case ServerType.Network:
                    return new Db4ORemoteDatabaseContext(settings, () => serverManager.ClientConfiguration);
                default:
                    throw new NotImplementedException("Unknown server type " + serverManager.Type);
            }
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
        }

        #endregion
    }
}