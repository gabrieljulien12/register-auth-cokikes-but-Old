using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using register_auth_cokikes.Entities;
using register_auth_cokikes.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace register_auth_cokikes.Controllers
{
    [Authorize]
    public class AccountControler : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IConfiguration _configuration;

        public AccountControler(DataBaseContext dataBaseContext, IConfiguration configuration)
        {
            _dataBaseContext = dataBaseContext;
            _configuration = configuration;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedpasword = Domd5hashedstring(model.Password);

                user user = _dataBaseContext.AuthUser.SingleOrDefault(x => x.UserName.ToLower() == model.Username.ToLower()
                && x.Password == hashedpasword);

                if (user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), "user is locked");
                        return View(model);
                    }
                    List<Claim> claims =
                    [
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("UserName", user.UserName),
                        new Claim(ClaimTypes.Name, user.Fullname ?? string.Empty),
                        new Claim(ClaimTypes.Role, user.Role),

                    ];
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "username or password is wrong");
                }


            }
            return View();
        }

        private string Domd5hashedstring(string s)
        {
            string md5salt = _configuration.GetValue<string>("Appsettings:MD5Salt");
            string salted = s + md5salt;
            string hashed = salted.MD5();
            return hashed;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dataBaseContext.AuthUser.Any(x => x.UserName.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "this user name is being used");
                    return View(model);

                }
                string hashedpasword = Domd5hashedstring(model.Password);
                user user = new()
                {
                    UserName = model.Username,
                    Password = hashedpasword
                };
                _dataBaseContext.AuthUser.Add(user);
                int affectrowcount = _dataBaseContext.SaveChanges();

                if (affectrowcount == 0)
                {
                    ModelState.AddModelError("", "user cannot be added");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View();


            public IActionResult Profile()
            {
                ProfileInfoLoader();

                return View();
            }

            private void ProfileInfoLoader()
            {
                Guid guid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                user user = _dataBaseContext.AuthUser.SingleOrDefault(x => x.Id == guid);

                ViewData["Fullname"] = user.Fullname;
            }

            public IActionResult Logout()
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(Login));

            }

            [HttpPost]
            public IActionResult ProfileChangeFullName([Required][StringLength(50)] string? FullName)
            {
                if (ModelState.IsValid)
                {
                    Guid guid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                    user user = _dataBaseContext.AuthUser.SingleOrDefault(x => x.Id == guid);

                    user.Fullname = FullName;
                    _dataBaseContext.SaveChanges();

                    ViewData["result"] = "FullnameUptade";


                }
                ProfileInfoLoader();
                return View("Profile");
            }
        }

        [HttpPost]
        private IActionResult ProfileChangePasword([MaxLength(16), MinLength(6), Required] string? password)
        {
            if (ModelState.IsValid)
            {
                Guid guid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                user user = _dataBaseContext.AuthUser.SingleOrDefault(x => x.Id == guid);
                string hashedpasword = Domd5hashedstring(password);
                string TempData = user.Password;
                user.Password = hashedpasword;

                if (hashedpasword != TempData)
                {
                    _dataBaseContext.SaveChanges();
                    ViewData["result"] = "Paswordchanced";
                }
                else if (hashedpasword == TempData)
                {
                    ModelState.AddModelError("", "zaten aynı şifreyi güncelemeye çalışıyorsunuz");


                }


            }

            ProfileInfoLoader();
            return View("Profile");
        }
    }
}
