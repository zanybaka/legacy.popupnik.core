using System;

namespace Popupnik.Server.Components.DataAccess.Exceptions
{
    internal sealed class DatabaseNotInitializedException : Exception
    {
        public DatabaseNotInitializedException()
        {
        }

        public DatabaseNotInitializedException(string message) : base(message)
        {
        }
    }
}