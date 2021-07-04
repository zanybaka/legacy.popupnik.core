using System;
using Popupnik.Server.Components.DataAccessBase;
using Popupnik.Server.Components.Model;

namespace Popupnik.Server.UnitTests.DataAccessTestsBase
{
    internal sealed class DatabaseContextMock : IDatabaseContext
    {
        #region Implementation of IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IDatabaseContext

        public ITable<T> Entities<T>()
            where T : IUniqueObject
        {
            throw new NotImplementedException();
        }

        public void SubmitChanges()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}