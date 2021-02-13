using AngularJsSample.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJsSample.Api.Models.Genres
{
    public class GenreViewModel
    {
        public int GenreId { get; set; }
        public UserViewModel UserCreated { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public UserViewModel UserLastModified { get; set; }
        public DateTimeOffset? DateLastModified { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}