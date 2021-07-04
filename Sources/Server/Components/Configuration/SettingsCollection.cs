using System.Configuration;

namespace Popupnik.Server.Components.Configuration
{
    public sealed class SettingsCollection
    {
        public SettingsBase Logger { get; set; }
        public SettingsBase Database { get; set; }

        public bool IsValid
        {
            get { return Logger != null && Database != null; }
        }
    }
}