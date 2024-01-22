using Carvilla.Helpers;
using Carvilla.Models;
using Carvilla.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carvilla.Controllers
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

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
		{
            RegisterVmValidator validations = new RegisterVmValidator();
            var res = await validations.ValidateAsync(vm);
            if (!ModelState.IsValid)
            {
                foreach (var eror in res.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(eror.PropertyName, eror.ErrorMessage);
                }
                return View(vm);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }


            AppUser user = new AppUser()
            {
                Email = vm.Email,
                Name = vm.Name,
                Surname = vm.Surname,
                UserName = vm.Username
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var er in result.Errors)
                {
                    ModelState.AddModelError("", er.Description);
                    return View(vm);
                }
            }

            await _signInManager.SignInAsync(user,isPersistent: false);
            await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

            return RedirectToAction("Index","Home");
		}
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm,string? returnUrl)
        {
            LoginVmValidator validations = new LoginVmValidator();
            var res = await validations.ValidateAsync(vm);
            if (!ModelState.IsValid)
            {
                foreach (var eror in res.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(eror.PropertyName, eror.ErrorMessage);
                }
                return View(vm);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await _userManager.FindByEmailAsync(vm.UserNameorEmail) ?? await _userManager.FindByNameAsync(vm.UserNameorEmail);
          
            if (user == null)
            {
                ModelState.AddModelError("", "Username/email or password is wrong");
                return View();
            }

           var result= await _signInManager.PasswordSignInAsync(user, vm.Password, false, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account banned for a few minutues");
                    return View(vm);
                }
                ModelState.AddModelError("", "Username/email or password is wrong");
                return View(vm);

            }

            if (returnUrl!=null && returnUrl.Contains("Login"))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index","Home");
        }
    }
}
