using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Mapping
{
    public static class MovieAuthorsMapper
    {
        public static Messaging.Views.MovieAuthors.MovieAuthor MapToView(this Model.MovieAuthors.MovieAuthor model)
        {
            return Mapper.Map<Messaging.Views.MovieAuthors.MovieAuthor>(model);
        }

        public static Model.MovieAuthors.MovieAuthor MapToModel (this Messaging.Views.MovieAuthors.MovieAuthor view)
        {
            return Mapper.Map<Model.MovieAuthors.MovieAuthor>(view);
        }

        public static List<Messaging.Views.MovieAuthors.MovieAuthor> MapToViews(this IEnumerable<Model.MovieAuthors.MovieAuthor> models)
        {
            var result = new List<Messaging.Views.MovieAuthors.MovieAuthor>();
            if (models == null) return result;
            result.AddRange(models.Select(item => item.MapToView()));
            return result;
        }
    }
}
