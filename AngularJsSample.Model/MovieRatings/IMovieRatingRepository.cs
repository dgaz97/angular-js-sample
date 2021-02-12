using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Model.MovieRatings
{
    public interface IMovieRatingRepository:IRepository<MovieRating, int>
    {
    }
}
