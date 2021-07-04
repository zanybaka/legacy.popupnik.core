using System;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;
using Popupnik.Server.Components.DataAccess.Exceptions;
using Popupnik.Server.Components.DataAccessBase;
using Popupnik.Server.Components.Model;
using Popupnik.Server.Components.Model.Entities;

namespace Popupnik.Server.Components.DataAccess
{
    public sealed class Db4ODatabaseServerManager : IDatabaseServerManager<IObjectServer, IServerConfiguration, IClientConfiguration>
    {
        private IObjectServer server;
        private bool isOpened;
        private readonly IDatabaseSettings settings;

        public Db4ODatabaseServerManager(
            IDatabaseSettings settings
            )
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            this.settings = settings;
        }

        public IServerConfiguration ServerConfiguration
        {
            get
            {
                Db4oFactory.Configure().ObjectClass(typeof (Model.Entities.User)).ObjectField(ModelConstants.UniqueFieldName).
                    Indexed(true);
                Db4oFactory.Configure().ObjectClass(typeof (Message)).ObjectField(ModelConstants.UniqueFieldName).Indexed(true);
                IServerConfiguration configuration = Db4oClientServer.NewServerConfiguration();
                configuration.File.GenerateUUIDs = ConfigScope.Disabled;
                configuration.File.GenerateVersionNumbers = ConfigScope.Disabled;
                return configuration;
            }
        }

        public IClientConfiguration ClientConfiguration
        {
            get
            {
                IClientConfiguration configuration = Db4oClientServer.NewClientConfiguration();
                return configuration;
            }
        }

        public ServerType Type { get; set; }

        #region Implementation of IDatabaseServerManager<IObjectServer>

        public void OpenServer(ServerType type)
        {
            if (isOpened)
            {
                throw new DatabaseServerAlreadyOpenedException();
            }
            switch (type)
            {
                case ServerType.Network:
                    //TODO: wrap with try/catch, add unit tests
                    server = Db4oClientServer.OpenServer(ServerConfiguration, settings.Path, settings.Port);
                    server.GrantAccess(settings.UserName, settings.Password);
                    break;
                case ServerType.Local:
                    //TODO: wrap with try/catch, add unit tests
                    server = Db4oClientServer.OpenServer(ServerConfiguration, settings.Path, 0);
                    break;
                default:
                    throw new NotImplementedException("Unsupported server type " + type);
            }
            Type = type;
            isOpened = true;
        }

        public void CloseServer()
        {
            if (server != null && isOpened)
            {
                server.Close();
            }
            Reset();
        }

        private void Reset()
        {
            server = null;
            Type = ServerType.Unknown;
            isOpened = false;
        }

        public IObjectServer GetServer()
        {
            return server;
        }

        public bool IsOpened
        {
            get { return isOpened; }
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (server != null)
            {
                //TODO: Check isOpened flag?
                server.Close();
            }
            Reset();
        }

        #endregion
    }
}