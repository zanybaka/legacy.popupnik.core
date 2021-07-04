using System.Collections.Generic;
using System.Linq;

namespace Popupnik.Server.Components.DataAccessBase
{
    public interface ITable<T> : IEnumerable<T>, IQueryable<T>
    {
        void InsertOnSubmit(T entity);
        void InsertAllOnSubmit(IEnumerable<T> entities);
        void DeleteOnSubmit(T entity);
        void DeleteAllOnSubmit(IEnumerable<T> entities);
        void UpdateOnCommit(T entity);
    }
}