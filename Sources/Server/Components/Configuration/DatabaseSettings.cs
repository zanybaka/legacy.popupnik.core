using Popupnik.Server.Components.DataAccess;

namespace Popupnik.Server.Components.Configuration
{
    internal sealed class DatabaseSettings : IDatabaseSettings
    {
        #region IDatabaseSettings Members

        [PropertyMappingAttribute("DatabaseFile")]
        public string Path { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        #endregion
    }
}