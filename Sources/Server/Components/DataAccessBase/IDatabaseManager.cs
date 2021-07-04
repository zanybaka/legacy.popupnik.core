using System;

namespace Popupnik.Server.Components.DataAccessBase
{
    public interface IDatabaseManager : IDisposable
    {
        bool IsDbExists();
        void KillDb();
        void CreateDb();
        void RecreateDb();
        bool ClearDb();
    }
}