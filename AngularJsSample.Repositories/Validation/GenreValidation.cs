using AngularJsSample.Model.Genres;
using AngularJsSample.Model.Movies;
using AngularJsSample.Model.Users;
using System;

namespace AngularJsSample.Repositories.Validation
{
    public static class GenreValidation
    {
        public static void CheckGenreForInsertOrUpdate(this Genre item)
        {
            if (item.Name == null || String.IsNullOrWhiteSpace(item.Name)) throw new Exception("Genre name can't be empty");
            if (item.Name.Length > 50)
                throw new Exception("Genre name can't be greater than 50 characters");
            if (item.Description == null) item.Description = "";
            if (item.Description.Length > 1000)
                throw new Exception("Genre description can't be greater than 1000 characters");
        }
    }
}
