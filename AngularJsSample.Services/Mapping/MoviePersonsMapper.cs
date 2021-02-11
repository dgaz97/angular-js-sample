using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Mapping
{
    public static class MoviePersonsMapper
    {
        public static Messaging.Views.MoviePersons.MoviePerson MapToView(this Model.MoviePersons.MoviePerson model)
        {
            return Mapper.Map<Messaging.Views.MoviePersons.MoviePerson>(model);
        }

        public static Model.MoviePersons.MoviePerson MapToModel (this Messaging.Views.MoviePersons.MoviePerson view)
        {
            return Mapper.Map<Model.MoviePersons.MoviePerson>(view);
        }

        public static List<Messaging.Views.MoviePersons.MoviePerson> MapToViews(this IEnumerable<Model.MoviePersons.MoviePerson> models)
        {
            var result = new List<Messaging.Views.MoviePersons.MoviePerson>();
            if (models == null) return result;
            result.AddRange(models.Select(item => item.MapToView()));
            return result;
        }
    }
}
