using System;

namespace Popupnik.Server.Components.DataAccess.Exceptions
{
    public sealed class DatabaseAlreadyExistException : Exception
    {
        public string Path { get; set; }

        public DatabaseAlreadyExistException(string path)
        {
            Path = path;
        }
    }
}