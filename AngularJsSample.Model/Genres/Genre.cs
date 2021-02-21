using AngularJsSample.Model.Users;
using System;

namespace AngularJsSample.Model.Genres
{
    /// <summary>
    /// Model class for genre
    /// </summary>
    public class Genre
    {
        public int GenreId { get; set; }
        public UserInfo UserCreated { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public UserInfo UserLastModified { get; set; }
        public DateTimeOffset? DateLastModified { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
