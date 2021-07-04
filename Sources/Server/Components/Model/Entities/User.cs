namespace Popupnik.Server.Components.Model.Entities
{
    public sealed class User : IUniqueObject
    {
        internal User()
        {
        }

        public string ComputerName { get; set; }
        public string DisplayName { get; set; }

        #region Implementation of IUniqueObject

        public long CreatedAt { get; set; }
        public long ModifiedAt { get; set; }

        #endregion
    }
}