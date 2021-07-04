namespace Popupnik.Server.Components.Model
{
    public interface IUniqueObject
    {
        long CreatedAt { get; set; }
        long ModifiedAt { get; set; }
    }
}