using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Services.Mapping
{
    public static class MoviesMapper
    {
        public static Messaging.Views.Movies.Movie MapToView (this Model.Movies.Movie model)
        {
            return Mapper.Map<Messaging.Views.Movies.Movie>(model);
        }

        public static Model.Movies.Movie MapToModel(this Messaging.Views.Movies.Movie view)
        {
            return Mapper.Map<Model.Movies.Movie>(view);
        }

        public static List<Messaging.Views.Movies.Movie> MapToViews(this IEnumerable<Model.Movies.Movie> models)
        {
            var result = new List<Messaging.Views.Movies.Movie>();
            if (models == null) return result;
            result.AddRange(models.Select(item => item.MapToView()));
            return result;
        }
    }
}
