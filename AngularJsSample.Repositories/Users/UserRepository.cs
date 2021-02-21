using System;
using System.Collections.Generic;
using System.Linq;
using AngularJsSample.Model.Users;
using Context = AngularJsSample.Repositories.DatabaseModel;

namespace AngularJsSample.Repositories
{
    /// <summary>
    /// Repository for users
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>List of users</returns>
        public List<Model.Users.UserInfo> FindAll()
        {
            using (var context = new Context.AngularJsSampleDbEntities())
            {
                return context.UserInfoes.MapToUserInfos();
            }
        }

        /// <summary>
        /// Gets user by ID
        /// </summary>
        /// <param name="key">User ID</param>
        /// <returns>User object</returns>
        public Model.Users.UserInfo FindBy(int key)
        {
            using (var context = new Context.AngularJsSampleDbEntities())
            {
                return context.UserInfoes.FirstOrDefault(user => user.Id == key).MapToUserInfo();
            }
        }
        /// <summary>
        /// Adds new user
        /// </summary>
        /// <param name="item">User object</param>
        /// <returns>ID of new user</returns>
        public int Add(UserInfo item)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="item">User object</param>
        /// <returns>true or false</returns>
        public bool Delete(UserInfo item)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="item">User object, with new data</param>
        /// <returns>User object</returns>
        public UserInfo Save(UserInfo item)
        {
            throw new NotImplementedException();
        }
    }
}
