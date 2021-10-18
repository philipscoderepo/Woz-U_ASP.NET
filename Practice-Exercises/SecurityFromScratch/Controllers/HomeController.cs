using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//added for login
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SecurityFromScratch.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            var claims = new List<Claim>
            {
                new Claim("user", "userName"),
                new Claim("role", "Admin"),
                new Claim("role", "PowerUser"),
                new Claim("xxx", "yyy")
            };

            await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        //[Authorize(Roles ="Admin")]
        //[Authorize(Roles = "Poweruser")]
        public IActionResult Secured()
        {
            return View();
        }
    }
}
