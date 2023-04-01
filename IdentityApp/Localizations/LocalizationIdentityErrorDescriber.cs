using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Localizations
{
    public class LocalizationIdentityErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Description = $"{userName}- Username baska bir kullanıcı tarafından alındı", Code = "DuplicateUser" };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Description = $"{email}- email baska bir kullanıcı tarafından alındı", Code = "DuplicateEmail" };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Description = $" password cok kısa  en az 6 karakter olmalıdır.", Code = "Password To Short" };
        }
      
    }
}
