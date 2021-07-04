using System;
using Db4objects.Db4o;
using Popupnik.Server.Components.DataAccess.Exceptions;
using Popupnik.Server.Components.DataAccessBase;
using Popupnik.Server.Components.Model;

namespace Popupnik.Server.Components.DataAccess
{
    //TODO: Rename to db4oDatabaseContext and split into local/remote
    public sealed class Db4OLocalDatabaseContext : IDatabaseContext
    {
        private IObjectContainer container;
        private readonly Func<IObjectServer> localServerGetter;

        public Db4OLocalDatabaseContext(
            Func<IObjectServer> localServerGetter
            )
        {
            if (localServerGetter == null)
            {
                throw new ArgumentNullException("localServerGetter");
            }
            this.localServerGetter = localServerGetter;
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
                    var localServer = localServerGetter();
                    if (localServer == null)
                    {
                        throw new DatabaseNotInitializedException("Database local server is not started.");
                    }
                    container = localServer.OpenClient();
                }
                return container;
            }
        }
    }
}