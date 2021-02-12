﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AngularJsSample.Repositories.DatabaseModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AngularJsSampleDbEntities : DbContext
    {
        public AngularJsSampleDbEntities()
            : base("name=AngularJsSampleDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
    
        public virtual int Genre_AddMovie(Nullable<int> movieId, Nullable<int> genreId, Nullable<int> userId)
        {
            var movieIdParameter = movieId.HasValue ?
                new ObjectParameter("MovieId", movieId) :
                new ObjectParameter("MovieId", typeof(int));
    
            var genreIdParameter = genreId.HasValue ?
                new ObjectParameter("GenreId", genreId) :
                new ObjectParameter("GenreId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Genre_AddMovie", movieIdParameter, genreIdParameter, userIdParameter);
        }
    
        public virtual int Genre_Delete(Nullable<int> genreId, Nullable<int> userLastModifiedId)
        {
            var genreIdParameter = genreId.HasValue ?
                new ObjectParameter("GenreId", genreId) :
                new ObjectParameter("GenreId", typeof(int));
    
            var userLastModifiedIdParameter = userLastModifiedId.HasValue ?
                new ObjectParameter("UserLastModifiedId", userLastModifiedId) :
                new ObjectParameter("UserLastModifiedId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Genre_Delete", genreIdParameter, userLastModifiedIdParameter);
        }
    
        public virtual int Genre_DeleteMovie(Nullable<int> movieId, Nullable<int> genreId, Nullable<int> userId)
        {
            var movieIdParameter = movieId.HasValue ?
                new ObjectParameter("MovieId", movieId) :
                new ObjectParameter("MovieId", typeof(int));
    
            var genreIdParameter = genreId.HasValue ?
                new ObjectParameter("GenreId", genreId) :
                new ObjectParameter("GenreId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Genre_DeleteMovie", movieIdParameter, genreIdParameter, userIdParameter);
        }
    
        public virtual ObjectResult<Genre_Get_Result> Genre_Get(Nullable<int> genreId)
        {
            var genreIdParameter = genreId.HasValue ?
                new ObjectParameter("GenreId", genreId) :
                new ObjectParameter("GenreId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Genre_Get_Result>("Genre_Get", genreIdParameter);
        }
    
        public virtual ObjectResult<Genre_GetAll_Result> Genre_GetAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Genre_GetAll_Result>("Genre_GetAll");
        }
    
        public virtual int Genre_Insert(Nullable<int> userCreatedId, string genreName, string genreDescription)
        {
            var userCreatedIdParameter = userCreatedId.HasValue ?
                new ObjectParameter("UserCreatedId", userCreatedId) :
                new ObjectParameter("UserCreatedId", typeof(int));
    
            var genreNameParameter = genreName != null ?
                new ObjectParameter("GenreName", genreName) :
                new ObjectParameter("GenreName", typeof(string));
    
            var genreDescriptionParameter = genreDescription != null ?
                new ObjectParameter("GenreDescription", genreDescription) :
                new ObjectParameter("GenreDescription", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Genre_Insert", userCreatedIdParameter, genreNameParameter, genreDescriptionParameter);
        }
    
        public virtual int Genre_Update(Nullable<int> genreId, Nullable<int> userLastModifiedId, string genreName, string genreDescription)
        {
            var genreIdParameter = genreId.HasValue ?
                new ObjectParameter("GenreId", genreId) :
                new ObjectParameter("GenreId", typeof(int));
    
            var userLastModifiedIdParameter = userLastModifiedId.HasValue ?
                new ObjectParameter("UserLastModifiedId", userLastModifiedId) :
                new ObjectParameter("UserLastModifiedId", typeof(int));
    
            var genreNameParameter = genreName != null ?
                new ObjectParameter("GenreName", genreName) :
                new ObjectParameter("GenreName", typeof(string));
    
            var genreDescriptionParameter = genreDescription != null ?
                new ObjectParameter("GenreDescription", genreDescription) :
                new ObjectParameter("GenreDescription", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Genre_Update", genreIdParameter, userLastModifiedIdParameter, genreNameParameter, genreDescriptionParameter);
        }
    
        public virtual int Movie_AddGenre(Nullable<int> movieId, Nullable<int> genreId, Nullable<int> userId)
        {
            var movieIdParameter = movieId.HasValue ?
                new ObjectParameter("MovieId", movieId) :
                new ObjectParameter("MovieId", typeof(int));
    
            var genreIdParameter = genreId.HasValue ?
                new ObjectParameter("GenreId", genreId) :
                new ObjectParameter("GenreId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Movie_AddGenre", movieIdParameter, genreIdParameter, userIdParameter);
        }
    
        public virtual int Movie_Delete(Nullable<int> userModifiedId, Nullable<int> movieId)
        {
            var userModifiedIdParameter = userModifiedId.HasValue ?
                new ObjectParameter("UserModifiedId", userModifiedId) :
                new ObjectParameter("UserModifiedId", typeof(int));
    
            var movieIdParameter = movieId.HasValue ?
                new ObjectParameter("MovieId", movieId) :
                new ObjectParameter("MovieId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Movie_Delete", userModifiedIdParameter, movieIdParameter);
        }
    
        public virtual int Movie_DeleteGenre(Nullable<int> movieId, Nullable<int> genreId, Nullable<int> userId)
        {
            var movieIdParameter = movieId.HasValue ?
                new ObjectParameter("MovieId", movieId) :
                new ObjectParameter("MovieId", typeof(int));
    
            var genreIdParameter = genreId.HasValue ?
                new ObjectParameter("GenreId", genreId) :
                new ObjectParameter("GenreId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Movie_DeleteGenre", movieIdParameter, genreIdParameter, userIdParameter);
        }
    
        public virtual ObjectResult<Movie_GetGenres_Result> Movie_GetGenres(Nullable<int> movieId)
        {
            var movieIdParameter = movieId.HasValue ?
                new ObjectParameter("MovieId", movieId) :
                new ObjectParameter("MovieId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Movie_GetGenres_Result>("Movie_GetGenres", movieIdParameter);
        }
    
        public virtual int Movie_Insert(Nullable<int> userCreatedId, string movieName, string movieDescription, Nullable<System.DateTimeOffset> movieReleaseDate, string moviePosterUrl, string movieImdbUrl)
        {
            var userCreatedIdParameter = userCreatedId.HasValue ?
                new ObjectParameter("UserCreatedId", userCreatedId) :
                new ObjectParameter("UserCreatedId", typeof(int));
    
            var movieNameParameter = movieName != null ?
                new ObjectParameter("MovieName", movieName) :
                new ObjectParameter("MovieName", typeof(string));
    
            var movieDescriptionParameter = movieDescription != null ?
                new ObjectParameter("MovieDescription", movieDescription) :
                new ObjectParameter("MovieDescription", typeof(string));
    
            var movieReleaseDateParameter = movieReleaseDate.HasValue ?
                new ObjectParameter("MovieReleaseDate", movieReleaseDate) :
                new ObjectParameter("MovieReleaseDate", typeof(System.DateTimeOffset));
    
            var moviePosterUrlParameter = moviePosterUrl != null ?
                new ObjectParameter("MoviePosterUrl", moviePosterUrl) :
                new ObjectParameter("MoviePosterUrl", typeof(string));
    
            var movieImdbUrlParameter = movieImdbUrl != null ?
                new ObjectParameter("MovieImdbUrl", movieImdbUrl) :
                new ObjectParameter("MovieImdbUrl", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Movie_Insert", userCreatedIdParameter, movieNameParameter, movieDescriptionParameter, movieReleaseDateParameter, moviePosterUrlParameter, movieImdbUrlParameter);
        }
    
        public virtual int Movie_Update(Nullable<int> userModifiedId, Nullable<int> movieId, string movieName, string movieDescription, Nullable<System.DateTimeOffset> movieReleaseDate, string moviePosterUrl, string movieImdbUrl)
        {
            var userModifiedIdParameter = userModifiedId.HasValue ?
                new ObjectParameter("UserModifiedId", userModifiedId) :
                new ObjectParameter("UserModifiedId", typeof(int));
    
            var movieIdParameter = movieId.HasValue ?
                new ObjectParameter("MovieId", movieId) :
                new ObjectParameter("MovieId", typeof(int));
    
            var movieNameParameter = movieName != null ?
                new ObjectParameter("MovieName", movieName) :
                new ObjectParameter("MovieName", typeof(string));
    
            var movieDescriptionParameter = movieDescription != null ?
                new ObjectParameter("MovieDescription", movieDescription) :
                new ObjectParameter("MovieDescription", typeof(string));
    
            var movieReleaseDateParameter = movieReleaseDate.HasValue ?
                new ObjectParameter("MovieReleaseDate", movieReleaseDate) :
                new ObjectParameter("MovieReleaseDate", typeof(System.DateTimeOffset));
    
            var moviePosterUrlParameter = moviePosterUrl != null ?
                new ObjectParameter("MoviePosterUrl", moviePosterUrl) :
                new ObjectParameter("MoviePosterUrl", typeof(string));
    
            var movieImdbUrlParameter = movieImdbUrl != null ?
                new ObjectParameter("MovieImdbUrl", movieImdbUrl) :
                new ObjectParameter("MovieImdbUrl", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Movie_Update", userModifiedIdParameter, movieIdParameter, movieNameParameter, movieDescriptionParameter, movieReleaseDateParameter, moviePosterUrlParameter, movieImdbUrlParameter);
        }
    
        public virtual int MoviePerson_Delete(Nullable<int> moviePersonId, Nullable<int> userLastModified)
        {
            var moviePersonIdParameter = moviePersonId.HasValue ?
                new ObjectParameter("MoviePersonId", moviePersonId) :
                new ObjectParameter("MoviePersonId", typeof(int));
    
            var userLastModifiedParameter = userLastModified.HasValue ?
                new ObjectParameter("UserLastModified", userLastModified) :
                new ObjectParameter("UserLastModified", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("MoviePerson_Delete", moviePersonIdParameter, userLastModifiedParameter);
        }
    
        public virtual ObjectResult<MoviePerson_Get_Result> MoviePerson_Get(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MoviePerson_Get_Result>("MoviePerson_Get", idParameter);
        }
    
        public virtual ObjectResult<MoviePerson_GetAll_Result> MoviePerson_GetAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MoviePerson_GetAll_Result>("MoviePerson_GetAll");
        }
    
        public virtual int MoviePerson_Insert(Nullable<int> userId, string firstName, string lastName, Nullable<System.DateTime> birthDate, string birthPlace, string biography, string imdbUrl, string imageUrl, Nullable<int> popularity)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var birthDateParameter = birthDate.HasValue ?
                new ObjectParameter("BirthDate", birthDate) :
                new ObjectParameter("BirthDate", typeof(System.DateTime));
    
            var birthPlaceParameter = birthPlace != null ?
                new ObjectParameter("BirthPlace", birthPlace) :
                new ObjectParameter("BirthPlace", typeof(string));
    
            var biographyParameter = biography != null ?
                new ObjectParameter("Biography", biography) :
                new ObjectParameter("Biography", typeof(string));
    
            var imdbUrlParameter = imdbUrl != null ?
                new ObjectParameter("ImdbUrl", imdbUrl) :
                new ObjectParameter("ImdbUrl", typeof(string));
    
            var imageUrlParameter = imageUrl != null ?
                new ObjectParameter("ImageUrl", imageUrl) :
                new ObjectParameter("ImageUrl", typeof(string));
    
            var popularityParameter = popularity.HasValue ?
                new ObjectParameter("Popularity", popularity) :
                new ObjectParameter("Popularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("MoviePerson_Insert", userIdParameter, firstNameParameter, lastNameParameter, birthDateParameter, birthPlaceParameter, biographyParameter, imdbUrlParameter, imageUrlParameter, popularityParameter);
        }
    
        public virtual int MoviePerson_Update(Nullable<int> moviePersonId, Nullable<int> userLastModified, string firstName, string lastName, Nullable<System.DateTime> birthDate, string birthPlace, string biography, string imdbUrl, string imageUrl, Nullable<int> popularity)
        {
            var moviePersonIdParameter = moviePersonId.HasValue ?
                new ObjectParameter("MoviePersonId", moviePersonId) :
                new ObjectParameter("MoviePersonId", typeof(int));
    
            var userLastModifiedParameter = userLastModified.HasValue ?
                new ObjectParameter("UserLastModified", userLastModified) :
                new ObjectParameter("UserLastModified", typeof(int));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var birthDateParameter = birthDate.HasValue ?
                new ObjectParameter("BirthDate", birthDate) :
                new ObjectParameter("BirthDate", typeof(System.DateTime));
    
            var birthPlaceParameter = birthPlace != null ?
                new ObjectParameter("BirthPlace", birthPlace) :
                new ObjectParameter("BirthPlace", typeof(string));
    
            var biographyParameter = biography != null ?
                new ObjectParameter("Biography", biography) :
                new ObjectParameter("Biography", typeof(string));
    
            var imdbUrlParameter = imdbUrl != null ?
                new ObjectParameter("ImdbUrl", imdbUrl) :
                new ObjectParameter("ImdbUrl", typeof(string));
    
            var imageUrlParameter = imageUrl != null ?
                new ObjectParameter("ImageUrl", imageUrl) :
                new ObjectParameter("ImageUrl", typeof(string));
    
            var popularityParameter = popularity.HasValue ?
                new ObjectParameter("Popularity", popularity) :
                new ObjectParameter("Popularity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("MoviePerson_Update", moviePersonIdParameter, userLastModifiedParameter, firstNameParameter, lastNameParameter, birthDateParameter, birthPlaceParameter, biographyParameter, imdbUrlParameter, imageUrlParameter, popularityParameter);
        }
    
        public virtual int MovieRating_Insert(Nullable<int> userCreatedId, Nullable<int> movieId, Nullable<decimal> userRating)
        {
            var userCreatedIdParameter = userCreatedId.HasValue ?
                new ObjectParameter("UserCreatedId", userCreatedId) :
                new ObjectParameter("UserCreatedId", typeof(int));
    
            var movieIdParameter = movieId.HasValue ?
                new ObjectParameter("MovieId", movieId) :
                new ObjectParameter("MovieId", typeof(int));
    
            var userRatingParameter = userRating.HasValue ?
                new ObjectParameter("UserRating", userRating) :
                new ObjectParameter("UserRating", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("MovieRating_Insert", userCreatedIdParameter, movieIdParameter, userRatingParameter);
        }
    
        public virtual ObjectResult<Genre_GetMovies_Result> Genre_GetMovies(Nullable<int> genreId)
        {
            var genreIdParameter = genreId.HasValue ?
                new ObjectParameter("GenreId", genreId) :
                new ObjectParameter("GenreId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Genre_GetMovies_Result>("Genre_GetMovies", genreIdParameter);
        }
    
        public virtual ObjectResult<Movie_Get_Result> Movie_Get(Nullable<int> movieId)
        {
            var movieIdParameter = movieId.HasValue ?
                new ObjectParameter("MovieId", movieId) :
                new ObjectParameter("MovieId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Movie_Get_Result>("Movie_Get", movieIdParameter);
        }
    
        public virtual ObjectResult<Movie_GetAll_Result> Movie_GetAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Movie_GetAll_Result>("Movie_GetAll");
        }
    
        public virtual ObjectResult<MovieRating_GetAll_Result> MovieRating_GetAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MovieRating_GetAll_Result>("MovieRating_GetAll");
        }
    
        public virtual ObjectResult<MovieRating_GetByMovie_Result> MovieRating_GetByMovie(Nullable<int> movieId)
        {
            var movieIdParameter = movieId.HasValue ?
                new ObjectParameter("MovieId", movieId) :
                new ObjectParameter("MovieId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MovieRating_GetByMovie_Result>("MovieRating_GetByMovie", movieIdParameter);
        }
    
        public virtual ObjectResult<MovieRating_GetByUser_Result> MovieRating_GetByUser(Nullable<int> userCreatedId)
        {
            var userCreatedIdParameter = userCreatedId.HasValue ?
                new ObjectParameter("UserCreatedId", userCreatedId) :
                new ObjectParameter("UserCreatedId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MovieRating_GetByUser_Result>("MovieRating_GetByUser", userCreatedIdParameter);
        }
    
        public virtual ObjectResult<MovieRating_GetByUserAndMovie_Result> MovieRating_GetByUserAndMovie(Nullable<int> userCreatedId, Nullable<int> movieId)
        {
            var userCreatedIdParameter = userCreatedId.HasValue ?
                new ObjectParameter("UserCreatedId", userCreatedId) :
                new ObjectParameter("UserCreatedId", typeof(int));
    
            var movieIdParameter = movieId.HasValue ?
                new ObjectParameter("MovieId", movieId) :
                new ObjectParameter("MovieId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MovieRating_GetByUserAndMovie_Result>("MovieRating_GetByUserAndMovie", userCreatedIdParameter, movieIdParameter);
        }
    }
}
