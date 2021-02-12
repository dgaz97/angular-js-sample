using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Services.Mapping
{
    public static class MovieRatingsMapper
    {
        public static Messaging.Views.MovieRatings.MovieRating MapToView (this Model.MovieRatings.MovieRating model)
        {
            return Mapper.Map<Messaging.Views.MovieRatings.MovieRating>(model);
        }

        public static Model.MovieRatings.MovieRating MapToModel(this Messaging.Views.MovieRatings.MovieRating view)
        {
            return Mapper.Map<Model.MovieRatings.MovieRating>(view);
        }

        public static List<Messaging.Views.MovieRatings.MovieRating> MapToViews(this IEnumerable<Model.MovieRatings.MovieRating> models)
        {
            var result = new List<Messaging.Views.MovieRatings.MovieRating>();
            if (models == null) return result;
            result.AddRange(models.Select(item => item.MapToView()));
            return result;
        }
    }
}
