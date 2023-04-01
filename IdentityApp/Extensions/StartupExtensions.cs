using IdentityApp.CustomeValidations;
using IdentityApp.Localizations;
using IdentityApp.Models;
using IdentityApp.MyContext;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IdentityApp.Extensions
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExtension(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;//Email uniq olsun mu ? 
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz123456789_.!*";// karakterleri girebilsin 
                options.Password.RequiredLength = 6;//pass 6 karakter olsun
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;//buyuk karkter olsunmu 
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);//yanlıs gırıs olursa 3 dakika kitler kullanıcıyı giriş yapamaz
                options.Lockout.MaxFailedAccessAttempts = 3;//kac basarısız girişte kitlensin ? 

            }).AddPasswordValidator<PasswordValidator>().
            AddUserValidator<UserValidator>().
            AddErrorDescriber<LocalizationIdentityErrorDescriber>().
            AddEntityFrameworkStores<ApplicationDbContext>();//password validator sınıfınıda eklıyorum

        }
    }
}
