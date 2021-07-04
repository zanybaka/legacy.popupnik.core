using Popupnik.Server.Components.Configuration;
using Popupnik.Server.Components.DataAccess;

namespace Popupnik.Server.UnitTests.DataAccessTestsBase
{
    internal sealed class TestDatabaseSettings : IDatabaseSettings
    {
        #region Implementation of IDatabaseSettings

        [PropertyMapping("DatabaseFile")]
        public string Path { get; set; }
        
        public int Port { get; set; }

        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        #endregion
    }
}