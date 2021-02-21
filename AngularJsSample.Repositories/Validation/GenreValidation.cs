using AngularJsSample.Model.Genres;
using AngularJsSample.Model.Movies;
using AngularJsSample.Model.Users;
using System;

namespace AngularJsSample.Repositories.Validation
{
    /// <summary>
    /// Static class for validating Genre objects before insert/update
    /// </summary>
    public static class GenreValidation
    {
        /// <summary>
        /// Checks whether Genre object is valid, throws exception if not
        /// </summary>
        /// <param name="item">Genre object</param>
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
