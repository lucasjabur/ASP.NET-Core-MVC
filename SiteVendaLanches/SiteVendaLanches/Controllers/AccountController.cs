using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteVendaLanches.ViewModel;

namespace SiteVendaLanches.Controllers {
    public class AccountController : Controller {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login() {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM) {
            if (!ModelState.IsValid) {
                return View(loginVM);
            }

            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user != null) {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded) {
                    
                    return RedirectToAction("Index", "Home"); 
                }    
            }
            ModelState.AddModelError("", "Falha ao realizar login!");
            return View(loginVM);
        }

        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM) {
            if (ModelState.IsValid) {
                var user = new IdentityUser { UserName = registroVM.UserName };
                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if (result.Succeeded) {
                    await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Login", "Account");
                }
                else {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");
                }

            }
            return View(registroVM);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout() {

            HttpContext.Session.Clear();
            HttpContext.User = null;

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() {
            return View();
        }
    }
}
