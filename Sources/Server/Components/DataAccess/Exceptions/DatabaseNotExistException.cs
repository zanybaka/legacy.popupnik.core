using System;

namespace Popupnik.Server.Components.DataAccess.Exceptions
{
    public sealed class DatabaseNotExistException : Exception
    {
        public string Path { get; set; }

        public DatabaseNotExistException(string path)
        {
            Path = path;
        }
    }
}