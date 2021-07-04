using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Db4objects.Db4o;
using Popupnik.Server.Components.DataAccessBase;
using Db4objects.Db4o.Linq;
using Popupnik.Server.Components.Model;

namespace Popupnik.Server.Components.DataAccess
{
    //TODO: Rename to db4oTable
    internal sealed class Db4OTable<T> : ITable<T>
        where T:IUniqueObject
    {
        private readonly IObjectContainer container;

        public Db4OTable(IObjectContainer container)
        {
            this.container = container;
        }

        //TODO: Check it
        public Expression Expression
        {
            get { return ((IQueryable)container.AsQueryable<T>()).Expression; }
        }

        //TODO: Check it
        public Type ElementType
        {
            get { return ((IQueryable)container.AsQueryable<T>()).ElementType; }
        }

        //TODO: Check it
        public IQueryProvider Provider
        {
            get { return ((IQueryable)container.AsQueryable<T>()).Provider; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return container.Query<T>().GetEnumerator();
        }

        public void InsertOnSubmit(T entity)
        {
            //The Stopwatch class assists the manipulation of timing-related performance
            //counters within managed code. Specifically, the Frequency field and
            //GetTimestamp method can be used in place of the unmanaged Win32 APIs
            //QueryPerformanceFrequency and QueryPerformanceCounter.

            //On a multiprocessor computer, it does not matter which processor the thread
            //runs on. However, because of bugs in the BIOS or the Hardware Abstraction
            //Layer (HAL), you can get different timing results on different processors.
            //To specify processor affinity for a thread, use the
            //ProcessThread..::.ProcessorAffinity method. 
            entity.CreatedAt = Stopwatch.GetTimestamp();
            container.Store(entity);
        }

        public void InsertAllOnSubmit(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                InsertOnSubmit(entity);
            }
        }

        public void DeleteOnSubmit(T entity)
        {
            container.Delete(entity);
        }

        public void DeleteAllOnSubmit(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                DeleteOnSubmit(entity);
            }
        }

        public void UpdateOnCommit(T entity)
        {
            //The Stopwatch class assists the manipulation of timing-related performance
            //counters within managed code. Specifically, the Frequency field and
            //GetTimestamp method can be used in place of the unmanaged Win32 APIs
            //QueryPerformanceFrequency and QueryPerformanceCounter.

            //On a multiprocessor computer, it does not matter which processor the thread
            //runs on. However, because of bugs in the BIOS or the Hardware Abstraction
            //Layer (HAL), you can get different timing results on different processors.
            //To specify processor affinity for a thread, use the
            //ProcessThread..::.ProcessorAffinity method. 
            entity.ModifiedAt = Stopwatch.GetTimestamp();
            container.Store(entity);
        }
    }
}