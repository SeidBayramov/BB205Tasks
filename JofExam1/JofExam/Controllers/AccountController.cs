using JofExam.Helpers;
using JofExam.Models;
using JofExam.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JofExam.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }
            AppUser user = new AppUser()
            {
                Surname = registerVm.Surname,
                Name = registerVm.Name,
                Email = registerVm.Email,
                UserName = registerVm.Username,
            };

            
            var result= await _userManager.CreateAsync(user,registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
            }
            await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
            await _signInManager.SignInAsync(user, false);
            
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm,string? returnurl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }
            var user = await _userManager.FindByEmailAsync(loginVm.UserNameorEmail);
            if (user == null)
            {
                user=await _userManager.FindByNameAsync(loginVm.UserNameorEmail);
            }
            if (user == null)
            {
                ModelState.AddModelError("", "UserName/Email or Password sehvdir");
            }
            var result= await _signInManager.PasswordSignInAsync(user,loginVm.Password,false,true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName/Email or Password sehvdir");
            }
            if (!result.IsLockedOut)
            {
                ModelState.AddModelError("", "Banlandiniz");
            }
             await _signInManager.SignInAsync(user, isPersistent: false);
            if (returnurl != null && returnurl.Contains("Login"))
            {
                return Redirect(returnurl);
            }
            return RedirectToAction(nameof(Index), "Home");

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Createrole()
        {
            foreach (var role in Enum.GetNames(typeof(UserRole)))
            {
                if(!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
