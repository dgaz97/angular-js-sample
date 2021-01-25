using Microsoft.AspNet.Identity;

namespace AngularJsSample.Authentication
{
    public class MyPasswordHasher : PasswordHasher
    {
        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return hashedPassword.Equals(HashPassword(providedPassword)) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }

        public override string HashPassword(string password)
        {
            return password;
        }
    }
}