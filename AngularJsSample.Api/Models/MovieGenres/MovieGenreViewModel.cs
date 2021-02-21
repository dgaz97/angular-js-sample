using AngularJsSample.Api.Models.Genres;
using AngularJsSample.Api.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJsSample.Api.Models.MovieGenres
{
    /// <summary>
    /// View model for movie-genre relation
    /// </summary>
    public class MovieGenreViewModel
    {
        public MovieViewModel Movie { get; set; }
        public GenreViewModel Genre { get; set; }
    }
}