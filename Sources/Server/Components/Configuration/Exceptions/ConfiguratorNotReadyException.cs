using System;

namespace Popupnik.Server.Components.Configuration.Exceptions
{
    public sealed class ConfiguratorNotReadyException : Exception
    {
        public ConfiguratorNotReadyException(string message) : base(message)
        {
        }
    }
}