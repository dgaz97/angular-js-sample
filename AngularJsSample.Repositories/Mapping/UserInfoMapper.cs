using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories
{
    public static class UserInfoMapper
    {
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
