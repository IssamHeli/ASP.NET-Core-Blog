using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Technexa.Data;
using Technexa.Models;

namespace Technexa.Views.Message
{
    public class MessageController : Controller
    {
        private readonly DBContextApplication _context;

        public MessageController(DBContextApplication context)
        {
            _context = context;
        }

        // GET: Message
        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");
            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }

            return _context.Messages != null ? 
                          View(await _context.Messages.OrderByDescending(m=>m.id).ToListAsync()) :
                          Problem("Entity set 'DBContextApplication.Messages'  is null.");
        }

        // GET: Message/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");
            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }

            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }
        /*
        // GET: Message/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Message/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,email,message")] Models.Message messagee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messagee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messagee);
        }
        */
        // GET: Message/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }

            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,email,message")] Models.Message messagee)
        {
            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }

            if (id != messagee.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messagee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(messagee.id))
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
            return View(messagee);
        }

        // GET: Message/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }

            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string? owner = HttpContext.Session.GetString("owner");

            if (owner == null)
            {
                return RedirectToAction(actionName: "index", controllerName: "Home");
            }

            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            if (_context.Messages == null)
            {
                return Problem("Entity set 'DBContextApplication.Messages'  is null.");
            }
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
          return (_context.Messages?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
