using AngularJsSample.Model.Genres;
using AngularJsSample.Model.MoviePersons;
using AngularJsSample.Model.MovieRatings;
using AngularJsSample.Model.MovieRoles;
using AngularJsSample.Model.Movies;
using AngularJsSample.Model.Users;
using AngularJsSample.Repositories;
using AngularJsSample.Repositories.Genres;
using AngularJsSample.Repositories.MoviePersons;
using AngularJsSample.Repositories.MovieRatings;
using AngularJsSample.Repositories.MovieRoles;
using AngularJsSample.Repositories.Movies;
using AngularJsSample.Services;
using AngularJsSample.Services.Impl;
using Autofac;

namespace AngularJsSample.Api.IoC
{
    /// <summary>
    /// Class with configuration for AutoFac
    /// </summary>
    public class CommonModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<MoviePersonRepository>().As<IMoviePersonRepository>();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>();
            builder.RegisterType<MovieRepository>().As<IMovieRepository>();
            builder.RegisterType<MovieRatingRepository>().As<IMovieRatingRepository>();
            builder.RegisterType<MovieRoleRepository>().As<IMovieRoleRepository>();

            //services
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<MoviePersonService>().As<IMoviePersonService>();
            builder.RegisterType<GenreService>().As<IGenreService>();
            builder.RegisterType<MovieService>().As<IMovieService>();
            builder.RegisterType<MovieRatingService>().As<IMovieRatingService>();
            builder.RegisterType<MovieRoleService>().As<IMovieRoleService>();
        }
    }
}