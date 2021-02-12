using AngularJsSample.Services.Messaging.MovieRatings.Requests;
using AngularJsSample.Services.Messaging.Views.MovieRatings;
using System.Collections.Generic;

namespace AngularJsSample.Services.Messaging.MovieRatings.Responses
{
    public class GetAllMovieRatingsResponse:ResponseBase<GetAllMovieRatingsRequest>
    {
        public List<MovieRating> MovieRatings { get; set; }
    }
}
