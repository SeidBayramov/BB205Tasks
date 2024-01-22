using ExamMaxim.Helpers;
using ExamMaxim.Models;
using ExamMaxim.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamMaxim.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _usermanger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, UserManager<AppUser> usermanger)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _usermanger = usermanger;
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
            AppUser user = new AppUser()
            {
                Email = vm.Email,
                Name = vm.Name,
                Surname = vm.Surname,
                UserName = vm.UserName,
            };

            var res= await _usermanger.CreateAsync(user, vm.Password);

            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            await _usermanger.AddToRoleAsync(user, UserRole.Admin.ToString());
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index","Home");
        }

        public IActionResult Login()
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVM, string? returnurl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser dbUser = await _usermanger.FindByNameAsync(loginVM.UserNameorEmail);
            if (dbUser is null)
            {
                dbUser = await _usermanger.FindByEmailAsync(loginVM.UserNameorEmail);
            }

            if (dbUser == null)
            {
                ModelState.AddModelError("", "UserName/Email  or Password dont'correct");
                return View(loginVM);
            }
            var result = await _signInManager.PasswordSignInAsync(dbUser, loginVM.Password,false, true);


            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your Account Is Lock Out");
                return View(loginVM);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName/Email  or Password dont'correct");
                return View();
            }

            await _signInManager.SignInAsync(dbUser, false);
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
        public async Task<IActionResult> CreateRole()
        {
            foreach (var roleName in Enum.GetNames(typeof(UserRole)))
            {
                if (await _roleManager.FindByNameAsync(roleName.ToString()) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = roleName.ToString(),
                    });
                }

            }
            return RedirectToAction(nameof(Index), "Home");
        }




    }
}
