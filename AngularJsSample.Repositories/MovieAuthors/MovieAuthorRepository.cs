using AngularJsSample.Model.MovieAuthors;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AngularJsSample.Repositories.MovieAuthors
{
    public class MovieAuthorRepository : IMovieAuthorRepository
    {
        public int Add(MovieAuthor item)
        {
            if (item.Id != 0) throw new Exception("Movie author ID must be null or 0");
            checkDataForInsertOrUpdate(item);
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieAuthor_Insert(item.UserCreated.Id, item.FirstName, item.LastName, item.BirthDate, item.BirthPlace, item.Biography, item.ImdbUrl, item.ImageUrl, item.Popularity);
            }
        }

        public bool Delete(MovieAuthor item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                if (FindBy(item.Id) == null) throw new Exception($"Author {item.Id} doesn't exist");
                context.MovieAuthor_Delete(item.Id, item.UserLastModified.Id);//Returns number of rows affected, should always be 1
                return true;
            }
        }

        public List<MovieAuthor> FindAll()
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieAuthor_GetAll().MapToModels();
            }
        }

        public MovieAuthor FindBy(int key)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieAuthor_Get(key).SingleOrDefault().MapToModel();
            }
        }

        public MovieAuthor Save(MovieAuthor item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                if (item.Id == null || item.Id <= 0) throw new Exception("Movie author ID can't be null or negative");
                if(FindBy(item.Id)==null) throw new Exception($"Author {item.Id} doesn't exist");
                checkDataForInsertOrUpdate(item);
                context.MovieAuthor_Update(item.Id, item.UserLastModified.Id, item.FirstName, item.LastName, item.BirthDate, item.BirthPlace, item.Biography, item.ImdbUrl, item.ImageUrl, item.Popularity);
                return FindBy(item.Id);
            }

        }
        private void checkDataForInsertOrUpdate(MovieAuthor item)
        {
            if (item.FirstName == null || String.IsNullOrWhiteSpace(item.FirstName)) throw new Exception("First name can't be empty");
            if (item.LastName == null || String.IsNullOrWhiteSpace(item.LastName)) throw new Exception("Last name can't be empty");
            if (item.BirthDate == DateTime.MinValue || item.BirthDate == null) throw new Exception("Birth date can't be empty");
            if (item.BirthPlace == null || String.IsNullOrWhiteSpace(item.BirthPlace)) throw new Exception("Birth place can't be empty");



            if (item.ImdbUrl == null || String.IsNullOrWhiteSpace(item.ImdbUrl)) throw new Exception("IMDb url can't be empty");
            Regex rxImdb = new Regex(@"^https?:\/\/(www\.)?imdb.com/");
            if (!rxImdb.IsMatch(item.ImdbUrl)) throw new Exception("IMDb url is invalid");

            if (!String.IsNullOrWhiteSpace(item.ImageUrl))
            {
                Regex rxHttp = new Regex(@"^https?:\/\/");
                Regex rxImage = new Regex(@"\.jpg$|\.jpeg$|\.png$|\.gif$");
                if (!rxHttp.IsMatch(item.ImageUrl) || !rxImage.IsMatch(item.ImageUrl)) throw new Exception("Image url is invalid");
            }

            if (item.Popularity == null || item.Popularity <= 0) throw new Exception("Popularity can't be empty, or less than 1");
        }
    }
}
