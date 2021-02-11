using AngularJsSample.Services.Messaging.Views.Users;
using AutoMapper;

namespace AngularJsSample.Services.Services.Mapping.Profiles
{
    public class CommonProfile: Profile
    {
        protected override void Configure()
        {
            //map from domain classes to views
            CreateMap<Model.MoviePersons.MoviePerson, Messaging.Views.MoviePersons.MoviePerson>();
            CreateMap<Model.Users.UserInfo, Messaging.Views.Users.UserInfo>();

            //map from views to domain classes
            CreateMap<Messaging.Views.MoviePersons.MoviePerson, Model.MoviePersons.MoviePerson>();
            CreateMap<Messaging.Views.Users.UserInfo, Model.Users.UserInfo>();
        }
    }
}
