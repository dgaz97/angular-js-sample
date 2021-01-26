using AngularJsSample.Model.MovieAuthors;
using AngularJsSample.Model.Users;
using AngularJsSample.Repositories;
using AngularJsSample.Repositories.MovieAuthors;
using AngularJsSample.Services;
using AngularJsSample.Services.Impl;
using Autofac;

namespace AngularJsSample.Api.IoC
{
    public class CommonModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<MovieAuthorRepository>().As<IMovieAuthorRepository>();

            //services
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<MovieAuthorService>().As<IMovieAuthorService>();
        }
    }
}