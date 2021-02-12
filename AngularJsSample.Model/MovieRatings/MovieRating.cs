using AngularJsSample.Model.Movies;
using AngularJsSample.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Model.MovieRatings
{
    public class MovieRating
    {
        public Movie Movie { get; set;}
        public UserInfo UserCreated { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public decimal UserRating { get; set; }
    }
}
