namespace Popupnik.Server.Components.DataAccess
{
    public interface IDatabaseSettings
    {
        string Path { get; set; }
        //TODO: Use SecureString
        int Port { get; set; }
        string Host { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}