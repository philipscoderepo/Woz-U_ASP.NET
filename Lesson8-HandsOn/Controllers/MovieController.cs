using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lesson8_HandsOn.Models;
//added for login
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Lesson8_HandsOn.Controllers
{
    //Handles operations with Movies
    public class MovieController : Controller
    {
        private readonly MovieContext _context;
        private readonly UserDataContext _userDataContext;
        
        public MovieController(MovieContext context, UserDataContext userDataContext)
        {
            _context = context;
            _userDataContext = userDataContext;
        }

        // GET: Movie
        //Available to anyone who accesses the website
        //---------------------------------------------------------

        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        //Registered User only pages
        //---------------------------------------------------------

        [Authorize]
        //Pulls the description for the movie at that id
        public async Task<IActionResult> MovieDescription(int? id)
        {
            if (id == null)
            {
                return View(null);
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return View(null);
            }

            return View(movie);
        }

        [Authorize]
        //User can watch the movie, and the movie gets added to their watch again list
        public async Task<IActionResult> Watch(int? id)
        {
            if (id == null)
            {
                return View(null);
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return View(null);
            }

            //get the current signed in user
            var username = User.Identity.Name;
            //if the user is not found in the DB then add an entry
            var userData = await _userDataContext.UserData.FirstOrDefaultAsync(m => m.Email == username);
            if (userData == null)
            {
                //first get the current list of users
                var users = await _userDataContext.UserData.ToListAsync();
                //create a new user
                UserData newUserEntry = new()
                {
                    Id = users.Count + 1,
                    Email = username,
                    MoviesWatched = id.ToString() + ","
                };

                await _userDataContext.AddAsync(newUserEntry);
                await _userDataContext.SaveChangesAsync();
            }
            else
            {
                var Ids = userData.ParseId();
                if (!Ids.Contains((int)id))
                {
                    userData.MoviesWatched += id.ToString() + ",";
                }
                _userDataContext.Update(userData);
                await _userDataContext.SaveChangesAsync();
            }

            return View(movie);
        }

        //Manager Only Pages
        //--------------------------------------------------------

        //list of movies available
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ModifyMovies()
        {
            return View(await _context.Movies.ToListAsync());
        }

        // GET: Movie/Details/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        
        // GET: Movie/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([Bind("Id,Title,LengthMin,Description,Img_Src")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,LengthMin,Description,Img_Src")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            return View(movie);
        }

        // GET: Movie/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
