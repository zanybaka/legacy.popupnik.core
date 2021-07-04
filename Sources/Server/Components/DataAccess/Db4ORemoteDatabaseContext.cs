using System;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;
using Popupnik.Server.Components.DataAccessBase;
using Popupnik.Server.Components.Model;

namespace Popupnik.Server.Components.DataAccess
{
    public sealed class Db4ORemoteDatabaseContext : IDatabaseContext
    {
        private readonly IDatabaseSettings settings;
        private IObjectContainer container;
        private readonly Func<IClientConfiguration> configuration;

        public Db4ORemoteDatabaseContext(
            IDatabaseSettings settings,
            Func<IClientConfiguration> configuration
            )
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            this.settings = settings;
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }
            this.configuration = configuration;
        }

        #region Implementation of IDatabaseContext

        public ITable<T> Entities<T>()
            where T:IUniqueObject
        {
            return new Db4OTable<T>(Container);
        }

        public void SubmitChanges()
        {
            Container.Commit();
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (container != null)
            {
                //'Close' method commits all changes, that is why we need to call 'Rollback'
                container.Rollback();
                container.Close();
            }
        }

        #endregion

        private IObjectContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = Db4oClientServer.OpenClient(configuration(), settings.Host, settings.Port,
                                                            settings.UserName, settings.Password);
                }
                return container;
            }
        }
    }
}