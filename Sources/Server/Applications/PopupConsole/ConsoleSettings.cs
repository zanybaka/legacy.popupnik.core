using Popupnik.Server.Components.ServerBase;

namespace Popupnik.Server.Applications.Console
{
    internal sealed class ConsoleSettings : IServerSettings
    {
        #region Implementation of IServerSettings

        public string MailSlotName { get; set; }

        #endregion
    }
}