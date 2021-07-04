using System;

namespace Popupnik.Server.Components.Model.Entities
{
    public sealed class Message : IUniqueObject
    {
        internal Message()
        {
        }

        public string Text { get; set; }
        public User From { get; set; }
        public User To { get; set; }
        public DateTime Time { get; set; }

        #region Implementation of IUniqueObject

        public long CreatedAt { get; set; }
        public long ModifiedAt { get; set; }

        #endregion
    }
}