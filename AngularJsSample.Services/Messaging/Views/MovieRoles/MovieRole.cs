using AngularJsSample.Services.Messaging.Views.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.Views.MovieRoles
{
    public class MovieRole
    {
        public int MovieRoleId { get; set; }
        public UserInfo UserCreated { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public UserInfo UserLastModified { get; set; }
        public DateTimeOffset? DateLastModified { get; set; }
        public string MovieRoleName { get; set; }
        public string MovieRoleDescription { get; set; }
        public int? MovieId { get; set; }
        public string MovieName { get; set; }
        public int? MoviePersonId { get; set; }
        public string MoviePersonName { get; set; }
    }
}
