using System;
using System.IO;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;
using Popupnik.Server.Components.DataAccess.Exceptions;
using Popupnik.Server.Components.DataAccessBase;
using Popupnik.Server.Components.Model.Entities;

namespace Popupnik.Server.Components.DataAccess
{
    public sealed class Db4ODatabaseManager : IDatabaseManager
    {
        #region Fields

        private IDatabaseSettings settings;
        
        #endregion

        #region Properties

        private IDatabaseSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    throw new DatabaseNotInitializedException();
                }
                return settings;
            }
            set { settings = value; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        #region .ctor

        public Db4ODatabaseManager(IDatabaseSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            Settings = settings;
        }


        #endregion

        #region Implementation of IDatabaseManager

        public bool IsDbExists()
        {
            return File.Exists(Settings.Path);
        }

        public void KillDb()
        {
            if (!File.Exists(Settings.Path))
            {
                throw new DatabaseNotExistException(Settings.Path);
            }
            File.Delete(Settings.Path);
        }

        //Configurations that influence the database file format will have to take place,
        //before a database is created, before the first #OpenXXX() call. Some examples:
        //Configuration configuration = Db4oFactory.Configure();
        //configuration.BlockSize(8);
        //configuration.Unicode(false); 
        public void CreateDb()
        {
            if (File.Exists(Settings.Path))
            {
                throw new DatabaseAlreadyExistException(Settings.Path);
            }
            var newContainer = Db4oFactory.OpenFile(Settings.Path);
            newContainer.Close();
        }

        public void RecreateDb()
        {
            if (IsDbExists())
            {
                KillDb();
            }
            CreateDb();
        }

        public bool ClearDb()
        {
/* TODO: throws null reference exception from db4o library... Update library to 8.0
            IObjectContainer db = Db4oFactory.OpenFile(Settings.Path);
            try
            {
                IObjectSet result = db.Query(typeof (object));
                foreach (object item in result)
                {
                    db.Delete(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Error on ClearDb:");
                Logger.WriteLine(ex.ToString());
                return false;
            }
            catch
            {
                Logger.WriteLine("Unknown error on ClearDb.");
                return false;
            }
            finally
            {
                db.Close();
            }
*/
            KillDb();
            CreateDb();
            return true;
        }

        #endregion
    }
}