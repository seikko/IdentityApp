using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.CustomeValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public  Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();
            if (password!.ToLower().Contains(user.UserName!.ToLower()))//eger şifrenin içinde username varsa ! null olamaz  run time da etkisi yok altını cızmesın dıye 
            {
                errors.Add(new() { Code = "PasswordContainUserName", Description = "Şifre Kullanıcı adı içeremez" });
            }
            if (password.ToLower().StartsWith("1234"))
            {
                errors.Add(new() { Code = "PasswordNotContain1234", Description = "Şifre 1234 içeremez içeremez" });
            }
            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
