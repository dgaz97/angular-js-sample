using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

using AngularJsSample.Api;
using AngularJsSample.Api.IoC;
using AngularJsSample.Api.Providers;
using AngularJsSample.Services.Services.Mapping.Profiles;

[assembly: OwinStartup(typeof(Startup))]
namespace AngularJsSample.Api
{
    public class Startup
    {
        private static IContainer _container;
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
            app.UseWebApi(config);

            try
            {
                var builder = new ContainerBuilder();
                builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
                builder.RegisterModule(new CommonModule());
                _container = builder.Build();
                config.DependencyResolver = new AutofacWebApiDependencyResolver(_container);

                Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile<CommonProfile>();
                });

                Mapper.AssertConfigurationIsValid();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}