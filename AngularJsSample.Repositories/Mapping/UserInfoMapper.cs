using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories
{
    /// <summary>
    /// Static class for mapping users between database results and model classes
    /// </summary>
    public static class UserInfoMapper
    {
        /// <summary>
        /// Maps User from database to model class
        /// </summary>
        /// <param name="model">User database result</param>
        /// <returns>User Model object</returns>
        public static Model.Users.UserInfo MapToUserInfo(this DatabaseModel.UserInfo model)
        {
            if (model == null)
                return null;
            return new Model.Users.UserInfo()
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }
        /// <summary>
        /// Maps list of Users from database to model class
        /// </summary>
        /// <param name="items">IEnumerable of User database results</param>
        /// <returns>List of User models</returns>
        public static List<Model.Users.UserInfo> MapToUserInfos(this IEnumerable<DatabaseModel.UserInfo> items)
        {
            var result = new List<Model.Users.UserInfo>();
            if (items == null)
                return result;
            result.AddRange(items.Select(u => u.MapToUserInfo()));
            return result;
        }
    }
}
