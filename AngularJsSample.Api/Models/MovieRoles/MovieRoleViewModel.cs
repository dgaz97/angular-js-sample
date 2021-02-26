using AngularJsSample.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJsSample.Api.Models.MovieRoles
{
    public class MovieRoleViewModel
    {
        public int MovieRoleId { get; set; }
        public UserViewModel UserCreated { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public UserViewModel UserLastModified { get; set; }
        public DateTimeOffset? DateLastModified { get; set; }
        public string MovieRoleName { get; set; }
        public string MovieRoleDescription { get; set; }
        public int? MovieId { get; set; }
        public string MovieName { get; set; }
        public int? MoviePersonId { get; set; }
        public string MoviePersonName { get; set; }
    }
}