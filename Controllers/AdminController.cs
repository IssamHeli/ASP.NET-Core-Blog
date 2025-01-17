using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Technexa.Data;
using Technexa.Models;

namespace Technexa.Views.admin
{
    public class AdminController : Controller
    {
        private readonly DBContextApplication _context;

        public AdminController(DBContextApplication context)
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("owner");
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("id,Username,keyenter")] Models.admin adminform)
        {
            ViewBag.Categories = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            if (ModelState.IsValid)
            {
               var Admin = await _context.admins.FirstOrDefaultAsync(a=>a.Username == adminform.Username && a.keyenter == adminform.keyenter);
                if(Admin != null)
                {

                    HttpContext.Session.SetString("owner", adminform.id.ToString()+ adminform.Username.ToString());

                    ViewBag.LoginAdmin = HttpContext.Session.GetString("owner");

                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
                return View(adminform);
            }
            return View();
        }
        
       
    }
}
