using AngularJsSample.Services.Messaging.Views.Genres;
using System.Collections.Generic;

namespace AngularJsSample.Services.Messaging.Genres
{
    public class GetAllGenresResponse:ResponseBase<GetAllGenresRequest>
    {
        public List<Genre> Genres { get; set; }
    }
}
