using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OauthServerCenter.ViewModels;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;

namespace OauthServerCenter.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestUserStore _users;

        public AccountController(TestUserStore users)
        {
            _users = users;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _users.FindByUsername(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "用户不存在.");
                    return View(model);
                }

                if (!_users.ValidateCredentials(model.Email, model.Password))
                {
                    ModelState.AddModelError("", "用户名或者密码不正确.");
                    return View(model);
                }

                
                await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(
                        HttpContext, 
                        new IdentityServer4.IdentityServerUser(user.SubjectId), 
                        new AuthenticationProperties() { 
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                            }
                    );

                return RedirectToAction("Index", "Home");

            }

            return View(model);
        }

        [HttpPost]
        public async Task Logout()
        {
            //HttpContext.Sign

           await HttpContext.SignOutAsync();
        }
    }
}
