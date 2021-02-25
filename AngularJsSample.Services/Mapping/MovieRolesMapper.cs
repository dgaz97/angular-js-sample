using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Mapping
{
    public static class MovieRolesMapper
    {
        public static Messaging.Views.MovieRoles.MovieRole MapToView (this Model.MovieRoles.MovieRole model)
        {
            return Mapper.Map<Messaging.Views.MovieRoles.MovieRole>(model);
        }
        public static Model.MovieRoles.MovieRole MapToModel(this Messaging.Views.MovieRoles.MovieRole view)
        {
            return Mapper.Map<Model.MovieRoles.MovieRole>(view);
        }

        public static List<Messaging.Views.MovieRoles.MovieRole> MapToViews(IEnumerable<Model.MovieRoles.MovieRole> models)
        {
            var result = new List<Messaging.Views.MovieRoles.MovieRole>();
            if (models == null) return result;
            result.AddRange(models.Select(item => item.MapToView()));
            return result;
        }
    }
}
