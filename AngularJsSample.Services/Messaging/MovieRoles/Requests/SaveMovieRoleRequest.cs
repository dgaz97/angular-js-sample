using AngularJsSample.Services.Messaging.Views.MovieRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.MovieRoles.Requests
{
    public class SaveMovieRoleRequest:RequestBase
    {
        public MovieRole MovieRole { get; set;}
    }
}
