using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Pustok_Temp.Helpers;

namespace Pustok_Temp.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _user;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> user, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_user = user;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		public IActionResult Register()
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
			AppUser appUser = new AppUser()
			{
				Fullname = registerVm.FullName,
				Email = registerVm.Email,
				UserName = registerVm.UserName,
			};
			var create = await _user.CreateAsync(appUser, registerVm.Password);
			if (!create.Succeeded)
			{
				foreach (var item in create.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
				return View();
			}

			await _user.AddToRoleAsync(appUser,Roles.Admin.ToString());
			
			return RedirectToAction("Index", "Home");
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVm LoginVm, string? returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			AppUser dbUser = await _user.FindByEmailAsync(LoginVm.EmailOrUserName);
			if (dbUser == null)
			{
				dbUser = await _user.FindByNameAsync(LoginVm.EmailOrUserName);
			}
			if (dbUser == null)
			{
                ModelState.AddModelError("", "Invalid Email or Username.");
                return View(LoginVm);
            }
			var result = await _signInManager.PasswordSignInAsync(dbUser, LoginVm.Password, LoginVm.RememberMe, true);
			if (!result.Succeeded)
			{
				ModelState.AddModelError("", "Email , Username of Password is wrong.");
				return View(LoginVm);
			}
			if (result.IsLockedOut)
			{
				ModelState.AddModelError("", "Your Account is Lock Out");
				return View(LoginVm);
			}
			if (returnUrl != null)
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> CreateRole()
		{
			foreach (var item in Enum.GetValues(typeof(Roles)))
			{
				if(await _roleManager.FindByNameAsync(item.ToString()) == null)
				{
					await _roleManager.CreateAsync(new IdentityRole()
					{
						Name = item.ToString(),

					});
				}
			}
			return RedirectToAction("Index", "Home");
		}
	}
}
