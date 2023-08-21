using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyRentalManagementWs.Models;

namespace PropertyRentalManagementWs.Controllers
{
    public class PropertyManagersController : Controller
    {
        private readonly PropertyRentalManagementWebSiteDBContext _context;

        public PropertyManagersController(PropertyRentalManagementWebSiteDBContext context)
        {
            _context = context;
        }

        // GET: PropertyManagers
        public async Task<IActionResult> Index()
        {
            return View(await _context.PropertyManager.ToListAsync());
        }

        // GET: PropertyManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManager
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyManager == null)
            {
                return NotFound();
            }

            return View(propertyManager);
        }

        // GET: PropertyManagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropertyManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Email")] PropertyManager propertyManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyManager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyManager);
        }

        // GET: PropertyManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManager.FindAsync(id);
            if (propertyManager == null)
            {
                return NotFound();
            }
            return View(propertyManager);
        }

        // POST: PropertyManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Email")] PropertyManager propertyManager)
        {
            if (id != propertyManager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyManagerExists(propertyManager.Id))
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
            return View(propertyManager);
        }

        // GET: PropertyManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManager
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyManager == null)
            {
                return NotFound();
            }

            return View(propertyManager);
        }

        // POST: PropertyManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyManager = await _context.PropertyManager.FindAsync(id);
            _context.PropertyManager.Remove(propertyManager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyManagerExists(int id)
        {
            return _context.PropertyManager.Any(e => e.Id == id);
        }
    }
}
