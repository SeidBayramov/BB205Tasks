using ExamAPP1.Entities;
using ExamAPP1.Helpers;
using ExamAPP1.Migrations;
using ExamAPP1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamAPP1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _rolemanager;

        public AccountController(RoleManager<IdentityRole> rolemanager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _rolemanager = rolemanager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AppUser appUser = new AppUser()
            {
                Name = vm.Name,
                Surname = vm.Surname,
                UserName = vm.UserName,
                Email = vm.Email,
            };
            var result = await _userManager.CreateAsync(appUser, vm.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View();
            }
            await _userManager.AddToRoleAsync(appUser, UserRole.Admin.ToString());

            await _signInManager.SignInAsync(appUser, false);
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByNameAsync(vm.UserNameorEmail) ?? await _userManager.FindByEmailAsync(vm.UserNameorEmail);

            if (user == null)
            {
                ModelState.AddModelError("", "UserName/Email  or Password dont'correct");
                return View(vm);
            }
            var res = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
            if (!res.Succeeded)
            {

                ModelState.AddModelError("", "UserName/Email  or Password dont'correct");
                return View(vm);
            }

            if (res.IsLockedOut)
            {
                ModelState.AddModelError("", "Account is locked");
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index), "Home");
        }


        public async Task<IActionResult> CreateRole()
        {
            foreach (var roleName in Enum.GetNames(typeof(UserRole)))
            {
                if (await _rolemanager.FindByNameAsync(roleName.ToString()) == null)
                {
                    await _rolemanager.CreateAsync(new IdentityRole()
                    {
                        Name = roleName.ToString(),
                    });
                }

            }
            return RedirectToAction(nameof(Index), "Home");
        }


    }
}