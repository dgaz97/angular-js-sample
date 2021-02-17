using AngularJsSample.Services.Messaging.Views.Genres;
using AngularJsSample.Services.Messaging.Views.Users;
using System;
using System.Collections.Generic;

namespace AngularJsSample.Services.Messaging.Views.Movies
{
    public class Movie
    {
        public int MovieId { get; set; }
        public UserInfo UserCreated { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public UserInfo UserLastModified { get; set; }
        public DateTimeOffset? DateLastModified { get; set; }
        public string MovieName { get; set; }
        public string MovieDescription { get; set; }
        public DateTime MovieReleaseDate { get; set; }
        public decimal MovieRating { get; set; }
        public string MoviePosterUrl { get; set; }
        public string MovieImdbUrl { get; set; }
        public List<Genre> Genres { get; set; }

    }
}
