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
            CreateMap<Model.Genres.Genre, Messaging.Views.Genres.Genre>();
            CreateMap<Model.Movies.Movie, Messaging.Views.Movies.Movie>();
            CreateMap<Model.MovieRatings.MovieRating, Messaging.Views.MovieRatings.MovieRating>();

            //map from views to domain classes
            CreateMap<Messaging.Views.MoviePersons.MoviePerson, Model.MoviePersons.MoviePerson>();
            CreateMap<Messaging.Views.Users.UserInfo, Model.Users.UserInfo>();
            CreateMap<Messaging.Views.Genres.Genre, Model.Genres.Genre>();
            CreateMap<Messaging.Views.Movies.Movie, Model.Movies.Movie>();
            CreateMap<Messaging.Views.MovieRatings.MovieRating, Model.MovieRatings.MovieRating>();
        }
    }
}
