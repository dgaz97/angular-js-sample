using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace AngularJsSample.Authentication
{
    public class MyUserManager : UserManager<MyUser, int>
    {
        #region constructors and destructors

        public MyUserManager(IUserStore<MyUser, int> store) : base(store)
        {
        }

        #endregion

        #region methods

        public static MyUserManager Create(IdentityFactoryOptions<MyUserManager> options, IOwinContext context)
        {
            var manager =
                new MyUserManager(
                    new UserStore<MyUser, MyRole, int, MyLogin, MyUserRole, MyClaim>(
                        context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<MyUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = false,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false,
                RequiredLength = 0
            };
            
            return manager;
        }

        public static MyUserManager Initialize()
        {
            var manager =
                new MyUserManager(
                    new UserStore<MyUser, MyRole, int, MyLogin, MyUserRole, MyClaim>(new ApplicationDbContext()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<MyUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            return manager;
        }

        #endregion
    }
}