using Db4objects.Db4o;
using Db4objects.Db4o.CS.Config;
using Popupnik.Server.Components.Configuration;
using Popupnik.Server.Components.DataAccess;
using Popupnik.Server.Components.DataAccessBase;
using Popupnik.Server.Components.Model;
using Popupnik.Server.UnitTests.DataAccessTestsBase.Properties;
using NUnit.Framework;

namespace Popupnik.Server.UnitTests.DataAccessTestsBase
{
    public abstract class DataAccessTestBase
    {
        protected static EntityFactory EntityFactory { get { return EntityFactory.Instance; } }
        protected IDatabaseContextProvider DbContextProvider { get; private set; }
        private IDatabaseServerManager<IObjectServer, IServerConfiguration, IClientConfiguration> dbServerManager;
        private IDatabaseManager dbManager;

        [SetUp]
        public void SetUp()
        {
            var databaseSettings = Configurator.MapProperties<TestDatabaseSettings>(Settings.Default);
            dbServerManager = new Db4ODatabaseServerManager(databaseSettings);
            dbManager = new Db4ODatabaseManager(databaseSettings);
            DbContextProvider = new Db4ODatabaseContextProvider(databaseSettings, dbServerManager);
            BeforeEachTest();
        }

        protected virtual void BeforeEachTest()
        {
            if (!dbManager.IsDbExists())
            {
                dbManager.CreateDb();
            }
            dbManager.ClearDb();

            dbServerManager.OpenServer(ServerType.Local);
        }

        [TearDown]
        public void TearDown()
        {
            AfterEachTest();
        }

        protected virtual void AfterEachTest()
        {
            dbManager.Dispose();
            DbContextProvider.Dispose();
            dbServerManager.CloseServer();
        }
    }
}
