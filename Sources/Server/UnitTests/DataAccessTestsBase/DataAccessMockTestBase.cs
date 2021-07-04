using System;
using Popupnik.Server.Components.DataAccessBase;
using Popupnik.Server.Components.Model;
using NUnit.Framework;

namespace Popupnik.Server.UnitTests.DataAccessTestsBase
{
    public abstract class DataAccessMockTestBase
    {
        protected EntityFactory EntityFactory;
        protected IDatabaseContextProvider DbContextProvider { get; private set; }

        [SetUp]
        public void SetUp()
        {
            DbContextProvider = new DatabaseContextProviderMock();
            BeforeEachTest();
        }

        protected virtual void BeforeEachTest()
        {
            throw new NotImplementedException("Implement mocks by DataSets, please.");
            //Configurator.InitializeComponents();
        }

        [TearDown]
        public void TearDown()
        {
            AfterEachTest();
        }

        protected virtual void AfterEachTest()
        {
        }
    }
}