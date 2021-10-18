using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lesson8_HandsOn.Models;
using Lesson8_HandsOn.Controllers;
//added for login
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Lesson8_HandsOn.Controllers
{
    //Handles operations with UserData
    public class UserDataController : Controller
    {
        private readonly UserDataContext _context;
        private readonly MovieContext _movieContext;

        public UserDataController(UserDataContext context, MovieContext movieContext) 
        {
            _context = context;
            _movieContext = movieContext;
        }

        //Registered User Only Pages
        //------------------------------------------------------------------

        [Authorize]
        public async Task<IActionResult> WatchAgain()
        {
            //get the user id from the current signed in user
            var userName = User.Identity.Name;

            if (!UserNameExists(userName))
            {
                return View(null);
            }

            var userData = await _context.UserData
            .FirstOrDefaultAsync(m => m.Email == userName);
            if (userData == null)
            {
                //they haven't watched any movies yet
                return View(null);
            }

            List<int> ids = userData.ParseId();
            //store the list of 
            List<Movie> moviesWatched = new List<Movie>();
            //get the list of movies from the DB
            var movies = await _movieContext.Movies.ToListAsync();
            foreach (var m in movies)
            {
                if (ids.Contains(m.Id))
                {
                    moviesWatched.Add(m);
                }
            }
            return View(moviesWatched);
        }

        //Manager Only Pages
        //------------------------------------------------------------------

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ViewUserData()
        {
            List<UserData> users = await _context.UserData.ToListAsync();
            List<Movie> movies = await _movieContext.Movies.ToListAsync();
            //<Email, Movie>
            Dictionary<string, List<Movie>> userData = new Dictionary<string, List<Movie>>();
            foreach (var u in users)
            {
                List<int> ids = u.ParseId();
                //store the list of 
                List<Movie> moviesWatched = new List<Movie>();
                //get the list of movies from the DB
                foreach (var m in movies)
                {
                    if (ids.Contains(m.Id))
                    {
                        moviesWatched.Add(m);
                    }
                }
                userData.Add(u.Email, moviesWatched);
            }

            return View(userData);
        }

        private bool UserNameExists(string email)
        {
            return _context.UserData.Any(e => e.Email == email);
        }

        private bool UserDataExists(int? id)
        {
            return _context.UserData.Any(e => e.Id == id);
        }

        //The functions below were auto generated and only used for testing
        //------------------------------------------------------------------

        // GET: UserData
        //use new keyword since we are inheriting 
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserData.ToListAsync());
        }

        // GET: UserData/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.UserData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // GET: UserData/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,MoviesWatched")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userData);
        }

        // GET: UserData/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.UserData.FindAsync(id);
            if (userData == null)
            {
                return NotFound();
            }
            return View(userData);
        }

        // POST: UserData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,MoviesWatched")] UserData userData)
        {
            if (id != userData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDataExists(userData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userData);
        }

        // GET: UserData/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.UserData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // POST: UserData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var userData = await _context.UserData.FindAsync(id);
            _context.UserData.Remove(userData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
