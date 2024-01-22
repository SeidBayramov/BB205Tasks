using DianaTemp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DianaTemp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
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
        public async Task<IActionResult> Register(RegisterVm regsiterVm)
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
                UserName = regsiterVm.Username
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

            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());


             string token= await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string url= _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext, action: "ConfirmEmail", controller: "Account",
                values:new { token,user.Id});

             SendMailService.SendMail(to: user.Email,  url: url) ;

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction(nameof(Index), "Home");
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
            AppUser dbUser = await _userManager.FindByNameAsync(loginVM.EmailOrUsername);
            if (dbUser is null)
            {
                dbUser = await _userManager.FindByEmailAsync(loginVM.EmailOrUsername);
            }

            if (dbUser == null)
            {
                ModelState.AddModelError("", "UserName/Email  or Password dont'correct");
                return View(loginVM);
            }
            var result = await _signInManager.PasswordSignInAsync(dbUser, loginVM.Password, loginVM.RememberMe, true);


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


            if(!await _userManager.IsEmailConfirmedAsync(dbUser))
            {
                ModelState.AddModelError("", "EmailConfirm must be");

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


        public async Task<IActionResult> ConfirmEmail(string id, string token)
        {
            if (id == null || token == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = await _userManager.FindByIdAsync(id);


            if (user == null)
            {

                return RedirectToAction("Error", "Home");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                
                ViewBag.IsSuccess = true;
            }
            else
            {
               
                ViewBag.IsSuccess = false;
            }

            return View();
        }
        public async Task<IActionResult> Subscribe(string email)
        {
            if (new System.Net.Mail.MailAddress(email).Address == email)
            {
                var existingUsers = await _userManager.Users.Where(u => u.Email == email).ToListAsync();

                if (existingUsers == null || !existingUsers.Any())
                {
                    var newUser = new AppUser { Email = email, UserName = email };
                    var result = await _userManager.CreateAsync(newUser);

                    if (result.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { id = newUser.Id, token }, protocol: HttpContext.Request.Scheme);

                        SendMailService.SendWelcomeMail(newUser.Email, callbackUrl);

                       
                        return await ConfirmEmail(newUser.Id, token);
                    }
                }
                else if (existingUsers.Any())
                {
                    foreach (var existingUser in existingUsers)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(existingUser);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { id = existingUser.Id, token }, protocol: HttpContext.Request.Scheme);

                        SendMailService.SendWelcomeMail(existingUser.Email, callbackUrl);

                        await ConfirmEmail(existingUser.Id, token);
                    }

                    ViewBag.SuccessMessage = $"Thank you! You have subscribed. Confirmation emails have been sent to all users with the email address {email}.";

                    return View();
                }
            }

            ViewBag.ErrorMessage = "Invalid email address.";
            return View();
        }


    }
}