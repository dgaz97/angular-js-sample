using System;
using AngularJsSample.Api.Models.Users;

namespace AngularJsSample.Api.Models.Movies
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }
        public UserViewModel UserCreated { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public UserViewModel UserLastModified { get; set; }
        public DateTimeOffset? DateLastModified { get; set; }
        public string MovieName { get; set; }
        public string MovieDescription { get; set; }
        public DateTime MovieReleaseDate { get; set; }
        public decimal MovieRating { get; set; }
        public string MoviePosterUrl { get; set; }
        public string MovieImdbUrl { get; set; }
    }
}