using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskBoardFinal.Data.Models;
using TaskBoardFinal.Models;

namespace TaskBoardFinal.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserController(UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //Here we set everything that we have in the RegisterViewModel
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //await signInManager.SignInAsync(user, isPersistent: false); => Directly logins after valid registration

                return RedirectToAction("Login", "User");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByNameAsync(model.FirstName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid login");
            return View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
