using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok_Temp.Helpers;
using Pustok_Temp.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Pustok_Temp.Controllers
{
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

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegsiterVm regsiterVm)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			AppUser user = new AppUser()
			{
				Email = regsiterVm.Email,
				Name = regsiterVm.Name,
				Surname = regsiterVm.Surname,
				UserName = regsiterVm.UserName
			};

			var result = await _userManager.CreateAsync(user, regsiterVm.Password);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}

				return View();
			}
			await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

			await _signInManager.SignInAsync(user, false);
			return RedirectToAction(nameof(Index), "Home");
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginVM loginVM, string? returnurl)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			AppUser dbUser = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
			if (dbUser is null)
			{
				dbUser = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
			}

			if (dbUser == null)
			{
				ModelState.AddModelError("", "UserName/Email  or Password dont'correct");
				return View(loginVM);
			}
			var result = await _signInManager.PasswordSignInAsync(dbUser, loginVM.Password, loginVM.IsRemember, true);


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

			return RedirectToAction(nameof(Index), "Home");
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
