using AngularJsSample.Services.Messaging.Views.Genres;

namespace AngularJsSample.Services.Messaging.Genres.Requests
{
    public class GetGenreResponse:ResponseBase<GetGenreRequest>
    {
        public Genre Genre { get; set; }
    }
}
