using AngularJsSample.Api.Models.Movies;
using AngularJsSample.Api.Models.Users;
using System;

namespace AngularJsSample.Api.Models.MovieRatings
{
    /// <summary>
    /// ViewModel for Movie Rating
    /// </summary>
    public class MovieRatingViewModel
    {
        public MovieViewModel Movie { get; set; }
        public UserViewModel UserCreated { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public decimal UserRating { get; set; }
    }
}