using AngularJsSample.Api.Models.Users;
using System;

namespace AngularJsSample.Api.Models
{
    public class MoviePersonViewModel
    {
        public int Id { get; set; }
        public UserViewModel UserCreated { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public UserViewModel UserLastModified { get; set; }
        public DateTimeOffset? DateLastModified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Biography { get; set; }
        public string ImdbUrl { get; set; }
        public string ImageUrl { get; set; }
        public int Popularity { get; set; }
    }
}