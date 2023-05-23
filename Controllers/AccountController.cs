using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudioWebApp.DAL;
using StudioWebApp.Models;
using StudioWebApp.ViewModels.AccountVM;

namespace StudioWebApp.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid) return View(registerVM);

            AppUser user =new AppUser()
            {
                Name= registerVM.Name,
                Surname=registerVM.Surname,
                Email=registerVM.Email,
                UserName=registerVM.UserName
            };

            IdentityResult result= await _userManager.CreateAsync(user, registerVM.Password);
            if(!result.Succeeded)
            {
                foreach(IdentityError? error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(Login));
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();

            AppUser user= await _userManager.FindByEmailAsync(login.Email);
            if(user == null) 
            {
                ModelState.AddModelError("", "Invalid password or email!");
                return View(login);
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user,login.Password,true,false);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Invalid password or email!");
                return View(login);
            }

            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
