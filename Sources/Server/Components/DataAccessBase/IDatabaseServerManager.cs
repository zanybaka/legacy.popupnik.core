using System;

namespace Popupnik.Server.Components.DataAccessBase
{
    public interface IDatabaseServerManager<TServer, TServerConfiguration, TClientConfiguration> : IDisposable
    {
        void OpenServer(ServerType type);
        void CloseServer();
        TServer GetServer();
        TServerConfiguration ServerConfiguration { get; }
        TClientConfiguration ClientConfiguration { get; }
        ServerType Type { get; } 
        bool IsOpened { get; }
    }
}