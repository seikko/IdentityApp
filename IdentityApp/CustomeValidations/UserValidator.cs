using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.CustomeValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();
            var isDijit = int.TryParse(user.UserName[0]!.ToString(), out _);// _ memory'de degisken tutulmicak il karakteri numeric olamaz
            if (isDijit) errors.Add(new() { Code = "Username contain first letter digit", Description = "Kullanıcı adınnı ilk karakteri sayısal karakter olamaz" });
            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
