using AngularJsSample.Services.Messaging.Genres.Requests;
using AngularJsSample.Services.Messaging.Views.Genres;

namespace AngularJsSample.Services.Messaging.Genres.Responses
{
    public class SaveGenreResponse:ResponseBase<SaveGenreRequest>
    {
        public Genre Genre { get; set; }
    }
}
