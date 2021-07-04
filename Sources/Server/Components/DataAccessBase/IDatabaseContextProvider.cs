using System;

namespace Popupnik.Server.Components.DataAccessBase
{
    public interface IDatabaseContextProvider : IDisposable
    {
        /// <summary>
        /// Creates new context instance (opens new client connection)
        /// Don't forget to dispose context!
        /// 'using' statement is recommened.
        /// </summary>
        IDatabaseContext CreateContext();
    }
}