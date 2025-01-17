
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Technexa.Data;

namespace Technexa.Views.Categorie
{
    public class CategorieController : Controller
    {
        private readonly DBContextApplication _context;

        public CategorieController(DBContextApplication context)
        {
            _context = context;
        }

        // GET: Categorie
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

            var posts = await _context.Categorie.OrderByDescending(p => p.id).ToListAsync();

            IPagedList<Models.Categorie> paginatedPosts = posts.ToPagedList(pageNumber, pageSize);

            return View(paginatedPosts);
        }

        // GET: Categorie/Details/5
        public async Task<IActionResult> Details(string id)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            if (id == null || _context.Categorie == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categorie
                .FirstOrDefaultAsync(m => m.id == id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        // GET: Categorie/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            return View();
        }

        // POST: Categorie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Designation")] Models.Categorie categorie)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(categorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categorie);
        }

        // GET: Categorie/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            if (id == null || _context.Categorie == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categorie.FindAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }
            return View(categorie);
        }

        // POST: Categorie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,Designation")] Models.Categorie categorie)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            if (id != categorie.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategorieExists(categorie.id))
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
            return View(categorie);
        }

        // GET: Categorie/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            if (id == null || _context.Categorie == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categorie
                .FirstOrDefaultAsync(m => m.id == id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        // POST: Categorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }
            if (_context.Categorie == null)
            {
                return Problem("Entity set 'DBContextApplication.Categorie'  is null.");
            }
            var categorie = await _context.Categorie.FindAsync(id);
            if (categorie != null)
            {
                _context.Categorie.Remove(categorie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategorieExists(string id)
        {
          return (_context.Categorie?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
