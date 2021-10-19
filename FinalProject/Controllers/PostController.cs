using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//added for login
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class PostController : Controller
    {
        private readonly PostContext _context;
        
        public PostController(PostContext context)
        {
            _context = context;
        }
        
        //GET: Post/DisplayPosts
        [HttpGet]
        public async Task<IActionResult> DisplayPosts()
        {
            var list = await _context.Posts.ToListAsync();
            list.Reverse();
            return View(list);
        }

        //Registered User Only Pages / Actions
        //-------------------------------------------------------

        //GET: Post/CreatePost
        [Authorize]
        public IActionResult CreatePost()
        {
            return View();
        }

        //POST: Post/CreatePost
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([Bind("Title, Content")] Post post)
        {
            post.Id = post.GetHashCode();
            post.TimeStamp = DateTime.Now.ToString();
            post.CreatedBy = User.Identity.Name;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("DisplayPosts");
        }

        //GET: Post/EditPost/{id}
        [Authorize]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if(post.CreatedBy != User.Identity.Name)
            {
                return Content("Not authorized");
            }

            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        //POST: Post/EidtPost/{id}
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditPost(int id, [Bind("Id, CreatedBy, TimeStamp, Title, Content")] Post post)
        {
            if (id != post.Id)
            {
                Console.WriteLine("Post ID != /ID: " + post.Id + " , " + id);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Update successful");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        Console.WriteLine("Post not found");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }   
                }
                return RedirectToAction("DisplayPosts");
            }
            return View(post);
        }

        //GET: Post/DeletePost/{id}
        [Authorize]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            if(User.Identity.Name != post.CreatedBy)
            {
                return Content("Not Authorized");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("DisplayPosts");
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
