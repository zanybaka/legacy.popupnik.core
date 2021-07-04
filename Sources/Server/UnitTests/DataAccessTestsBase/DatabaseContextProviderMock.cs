using Popupnik.Server.Components.DataAccessBase;

namespace Popupnik.Server.UnitTests.DataAccessTestsBase
{
    internal sealed class DatabaseContextProviderMock : IDatabaseContextProvider
    {
        private IDatabaseContext context;

        #region Implementation of IDisposable

        public void Dispose()
        {
        }

        #endregion

        #region Implementation of IDatabaseContextProvider

        public IDatabaseContext CreateContext()
        {
            if (context == null)
            {
                context = new DatabaseContextMock();
            }
            return context;
        }

        #endregion
    }
}