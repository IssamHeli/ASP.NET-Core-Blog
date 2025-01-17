using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Technexa.Data;
using Technexa.Models;

namespace Technexa.Views.Post
{
    public class PostController : Controller
    {
        private readonly DBContextApplication _context;

        public PostController(DBContextApplication context)
        {
            _context = context;
        }

        // GET: Post
        public async Task<IActionResult> Index(int? page)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            var posts = await _context.Posts.OrderByDescending(p => p.DateCreated).ToListAsync();

            IPagedList<Models.Post> paginatedPosts = posts.ToPagedList(pageNumber, pageSize);

            return View(paginatedPosts);
            
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.idpost == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idpost,Title,Description,SrcYoutubeVedio,Srcimage,DateCreated,categorie")] Models.Post post)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idpost,Title,Description,SrcYoutubeVedio,Srcimage,DateCreated,categorie")] Models.Post post)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            if (id != post.idpost)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.idpost))
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
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.idpost == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            if (_context.Posts == null)
            {
                return Problem("Entity set 'DBContextApplication.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.idpost == id)).GetValueOrDefault();
        }




    }
}
