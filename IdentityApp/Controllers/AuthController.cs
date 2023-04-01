using IdentityApp.Extensions;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager; //auth işlemleri

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            var identityResult = await _userManager.CreateAsync(new() { UserName = request.UserName, PhoneNumber = request.PhoneNumber, Email = request.Email }, request.PasswordConfirm);

            if(identityResult.Succeeded)
            {
                ViewBag.SuccessMessage = "Kayıt işlemi başarılı";
                return View();
            }

            ModelState.AddModelErrorList(identityResult.Errors.Select(x=> x.Description).ToList()) ;
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request,string? returnUrl = null)
        {

            returnUrl =returnUrl ?? Url.Action("Index","Home");
            var isUser = await  _userManager.FindByEmailAsync(request.Email!);
            if(isUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya şifre yanlış");
            }

            var result = await _signInManager.PasswordSignInAsync(isUser!, request.Password!, request.RememberMe, true);
            //isPersistent cookie'de tutayımmı diyor beni hatırla checkbox'ı
            //lockoutOnFailure  3 kere giriş yaptıgında kitleme yapıyıyoruz identity'nin default kitleme metodunu aktif ediyoruz bunuda customize edebiliyoruz
            if (result.Succeeded)
            {
                return Redirect(returnUrl!);
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string> { "3 dakika giriş yapamazsınız" });
                return View();
            }
            ModelState.AddModelErrorList(new List<string>() { $"Email veya şifre yanlış", $"Başarısız giriş sayısı = {await _userManager.GetAccessFailedCountAsync(isUser)} - Süresi {await _userManager.GetLockoutEndDateAsync(isUser)}" });
            return View();
        }
        /*
         Access fail count basarısız giriş işlemleri tutar
        LockoutEnable kitlimi degilmi login
        LockoutEnd Kilit ne zaman acılır ?
         
         */

    }
}
