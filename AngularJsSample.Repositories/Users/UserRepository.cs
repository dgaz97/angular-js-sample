using System;
using System.Collections.Generic;
using System.Linq;
using AngularJsSample.Model.Users;
using Context = AngularJsSample.Repositories.DatabaseModel;

namespace AngularJsSample.Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<Model.Users.UserInfo> FindAll()
        {
            using (var context = new Context.AngularJsSampleDbEntities())
            {
                return context.UserInfoes.MapToUserInfos();
            }
        }

        public Model.Users.UserInfo FindBy(int key)
        {
            using (var context = new Context.AngularJsSampleDbEntities())
            {
                return context.UserInfoes.FirstOrDefault(user => user.Id == key).MapToUserInfo();
            }
        }

        public int Add(UserInfo item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(UserInfo item)
        {
            throw new NotImplementedException();
        }

        public UserInfo Save(UserInfo item)
        {
            throw new NotImplementedException();
        }
    }
}
