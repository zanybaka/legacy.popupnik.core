using System;
using Popupnik.Server.Components.Model;

namespace Popupnik.Server.Components.DataAccessBase
{
    public interface IDatabaseContext : IDisposable
    {
        ITable<T> Entities<T>()
            where T : IUniqueObject;
        void SubmitChanges();
    }
}