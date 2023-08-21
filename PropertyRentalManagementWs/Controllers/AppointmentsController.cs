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
    public class AppointmentsController : Controller
    {
        private readonly PropertyRentalManagementWebSiteDBContext _context;

        public AppointmentsController(PropertyRentalManagementWebSiteDBContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var propertyRentalManagementWebSiteDBContext = _context.Appointment.Include(a => a.Apartment).Include(a => a.PotentialTenant).Include(a => a.PropertyManager);
            return View(await propertyRentalManagementWebSiteDBContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Apartment)
                .Include(a => a.PotentialTenant)
                .Include(a => a.PropertyManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "Id", "Img1");
            ViewData["PotentialTenantId"] = new SelectList(_context.PotentialTenant, "Id", "Email");
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManager, "Id", "Email");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PotentialTenantId,PropertyManagerId,ApartmentId,DateTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "Id", "Img1", appointment.ApartmentId);
            ViewData["PotentialTenantId"] = new SelectList(_context.PotentialTenant, "Id", "Email", appointment.PotentialTenantId);
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManager, "Id", "Email", appointment.PropertyManagerId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "Id", "Img1", appointment.ApartmentId);
            ViewData["PotentialTenantId"] = new SelectList(_context.PotentialTenant, "Id", "Email", appointment.PotentialTenantId);
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManager, "Id", "Email", appointment.PropertyManagerId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PotentialTenantId,PropertyManagerId,ApartmentId,DateTime")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "Id", "Img1", appointment.ApartmentId);
            ViewData["PotentialTenantId"] = new SelectList(_context.PotentialTenant, "Id", "Email", appointment.PotentialTenantId);
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManager, "Id", "Email", appointment.PropertyManagerId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Apartment)
                .Include(a => a.PotentialTenant)
                .Include(a => a.PropertyManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.Id == id);
        }
    }
}
