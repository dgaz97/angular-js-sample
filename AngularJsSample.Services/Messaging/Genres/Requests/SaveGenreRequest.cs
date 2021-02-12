using AngularJsSample.Services.Messaging.Views.Genres;

namespace AngularJsSample.Services.Messaging.Genres.Requests
{
    public class SaveGenreRequest:RequestBase
    {
        public Genre Genre { get; set; }
    }
}
