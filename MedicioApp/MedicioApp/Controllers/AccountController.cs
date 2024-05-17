using Core.Models;
using MedicioApp.DTO_s;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicioApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager = null, RoleManager<IdentityRole> roleManager = null)
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
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid) return View();
            User appUser = new User()
            {
                Name = registerDTO.Firstname,
                Surname = registerDTO.Lastname,
                Email = registerDTO.Email,
                UserName = registerDTO.Username
            };
            var result = await _userManager.CreateAsync(appUser, registerDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(appUser, "Member");

            return RedirectToAction("Login");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid) return View();

            User appUser;

            if (loginDTO.UsernameOrEmail.Contains("@"))
            {
                appUser = await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);
            }
            else
            {
                appUser = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);
            }
            if (appUser == null)
            {
                ModelState.AddModelError("", "UsernameOrEmail ve ya Password sehvdir");
                return View();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginDTO.Password, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan yeniden cehd edin");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UsernameOrEmail ve ya Password sehvdir!");
                return View();
            }
            await _signInManager.SignInAsync(appUser, loginDTO.RememberMe);
            var roles = await _userManager.GetRolesAsync(appUser);
            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Doctor", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role1 = new IdentityRole("Admin");
            IdentityRole role2 = new IdentityRole("Moderator");
            IdentityRole role3 = new IdentityRole("Member");
            await _roleManager.CreateAsync(role1);
            await _roleManager.CreateAsync(role2);
            await _roleManager.CreateAsync(role3);
            return Ok();

        }
    }
}
