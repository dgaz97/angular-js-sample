using System;
using AngularJsSample.Services.Messaging.Views.Movies;
using AngularJsSample.Services.Messaging.Views.Users;

namespace AngularJsSample.Services.Messaging.Views.MovieRatings
{
    public class MovieRating
    {
        public Movie Movie { get; set;}
        public UserInfo UserCreated { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public decimal UserRating { get; set; }
    }
}
