using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Mapping
{
    public static class GenresMapper
    {
        public static Messaging.Views.Genres.Genre MapToView (this Model.Genres.Genre model)
        {
            return Mapper.Map<Messaging.Views.Genres.Genre>(model);
        }

        public static Model.Genres.Genre MapToModel(this Messaging.Views.Genres.Genre view)
        {
            return Mapper.Map<Model.Genres.Genre>(view);
        }

        public static List<Messaging.Views.Genres.Genre> MapToViews(this IEnumerable<Model.Genres.Genre> models)
        {
            var result = new List<Messaging.Views.Genres.Genre>();
            if (models == null) return result;
            result.AddRange(models.Select(item => item.MapToView()));
            return result;
        }
    }
}
