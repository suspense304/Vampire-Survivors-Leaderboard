using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vampire_Survivors_Leaderboard.Data
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        public IDataProtectionProvider Provider { get; }

        public AccountController(IDataProtectionProvider provider)
        {
            Provider = provider;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, "Discord");
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            //This removes the cookie assigned to the user login.
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect(returnUrl);
        }
    }
}
