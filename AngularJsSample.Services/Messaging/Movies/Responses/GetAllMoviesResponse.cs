﻿using AngularJsSample.Services.Messaging.Movies.Requests;
using AngularJsSample.Services.Messaging.Views.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.Movies.Responses
{
    public class GetAllMoviesResponse:ResponseBase<GetAllMoviesRequest>
    {
        public List<Movie> Movies { get; set; }
    }
}
