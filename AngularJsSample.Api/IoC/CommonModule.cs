using AngularJsSample.Model.Users;
using AngularJsSample.Repositories;
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

            //services
            builder.RegisterType<UserService>().As<IUserService>();
        }
    }
}