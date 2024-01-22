using Carvilla.Helpers;
using Carvilla.Models;
using Carvilla.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carvilla.Areas.Manage.Controllers
{

    public class CarController1 : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _rolemanager;

        public CarController1(UserManager<AppUser> usermanager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> rolemanager)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _rolemanager = rolemanager;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            RegisterVmValidator validations = new RegisterVmValidator();
            var res = validations.Validate(vm);
            if (!ModelState.IsValid)
            {
                foreach (var eror in res.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(eror.PropertyName, eror.ErrorMessage);
                    return View(vm);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AppUser user = new AppUser()
            {
                Email = vm.Email,
                Surname = vm.Surname,
                Name = vm.Name,
                UserName = vm.Username
            };
            var result = await _usermanager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View(vm);

                }

            }
            await _usermanager.AddToRoleAsync(user, UserRole.Admin.ToString());
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Login()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm, string? returnurl)
        {
            LoginVmValidator validations = new LoginVmValidator();
            var res = validations.Validate(vm);
            if (!ModelState.IsValid)
            {
                foreach (var eror in res.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(eror.PropertyName, eror.ErrorMessage);
                    return View(vm);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await _usermanager.FindByEmailAsync(vm.UserNameorEmail) ?? await _usermanager.FindByNameAsync(vm.UserNameorEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "Username/Email or password is wromg");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Banlandiniz");
                    return View(vm);
                }
                ModelState.AddModelError("", "Username/Email or password is wromg");
                return View(vm);
            }

            if (returnurl != null && returnurl.Contains("Login"))
            {
                return Redirect(returnurl);
            }

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetNames(typeof(UserRole)))
            {
                if (!await _rolemanager.RoleExistsAsync(item))
                {
                    await _rolemanager.CreateAsync(new IdentityRole(item));
                }

            }
            return RedirectToAction(nameof(Index));
        }

    }
}
