using Agency.Busines.Services.Interface;
using Agency.Busines.ViewModel.Account;
using Agency.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agency.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(IAccountService accountService, SignInManager<AppUser> signInManager)
        {
            _accountService = accountService;
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
            RegisterResult registerResult = await _accountService.RegisterAsync(vm);
            if (!registerResult.Result.Succeeded)
            {
                foreach (var error in registerResult.Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(vm);
            }

            await _signInManager.SignInAsync(registerResult.User, false);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            AppUser user = await _accountService.ValidateUserCredentialsAsync(loginVm);

            if (user == null)
            {
                ModelState.AddModelError("", "Username Or Password is wrong");
                return View(loginVm);
            }

            var res = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);

            if (!res.Succeeded)
            {
                ModelState.AddModelError("", "Username Or Password is wrong");
                return View(loginVm);
            }

            if (res.IsLockedOut)
            {
                ModelState.AddModelError("", "Account is locked");
                return View(loginVm);
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRole()
        {
            await _accountService.CreateRoleAsync();

            return RedirectToAction("Index", "Home");
        }


    }
}

