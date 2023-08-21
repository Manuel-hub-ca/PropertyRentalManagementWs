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
    public class PotentialTenantsController : Controller
    {
        private readonly PropertyRentalManagementWebSiteDBContext _context;

        public PotentialTenantsController(PropertyRentalManagementWebSiteDBContext context)
        {
            _context = context;
        }

        // GET: PotentialTenants
        public async Task<IActionResult> Index()
        {
            return View(await _context.PotentialTenant.ToListAsync());
        }

        // GET: PotentialTenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var potentialTenant = await _context.PotentialTenant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (potentialTenant == null)
            {
                return NotFound();
            }

            return View(potentialTenant);
        }

        // GET: PotentialTenants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PotentialTenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Email")] PotentialTenant potentialTenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(potentialTenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(potentialTenant);
        }

        // GET: PotentialTenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var potentialTenant = await _context.PotentialTenant.FindAsync(id);
            if (potentialTenant == null)
            {
                return NotFound();
            }
            return View(potentialTenant);
        }

        // POST: PotentialTenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Email")] PotentialTenant potentialTenant)
        {
            if (id != potentialTenant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(potentialTenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PotentialTenantExists(potentialTenant.Id))
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
            return View(potentialTenant);
        }

        // GET: PotentialTenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var potentialTenant = await _context.PotentialTenant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (potentialTenant == null)
            {
                return NotFound();
            }

            return View(potentialTenant);
        }

        // POST: PotentialTenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var potentialTenant = await _context.PotentialTenant.FindAsync(id);
            _context.PotentialTenant.Remove(potentialTenant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PotentialTenantExists(int id)
        {
            return _context.PotentialTenant.Any(e => e.Id == id);
        }
    }
}
