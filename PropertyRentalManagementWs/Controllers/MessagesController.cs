﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyRentalManagementWs.Models;

namespace PropertyRentalManagementWs.Controllers
{
    public class MessagesController : Controller
    {
        private readonly PropertyRentalManagementWebSiteDBContext _context;

        public MessagesController(PropertyRentalManagementWebSiteDBContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var propertyRentalManagementWebSiteDBContext = _context.Message.Include(m => m.PotentialTenant).Include(m => m.PropertyManager);
            return View(await propertyRentalManagementWebSiteDBContext.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.PotentialTenant)
                .Include(m => m.PropertyManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            ViewData["PotentialTenantId"] = new SelectList(_context.PotentialTenant, "Id", "Email");
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManager, "Id", "Email");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PotentialTenantId,PropertyManagerId,Subject,Content,DateTime")] Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PotentialTenantId"] = new SelectList(_context.PotentialTenant, "Id", "Email", message.PotentialTenantId);
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManager, "Id", "Email", message.PropertyManagerId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["PotentialTenantId"] = new SelectList(_context.PotentialTenant, "Id", "Email", message.PotentialTenantId);
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManager, "Id", "Email", message.PropertyManagerId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PotentialTenantId,PropertyManagerId,Subject,Content,DateTime")] Message message)
        {
            if (id != message.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.Id))
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
            ViewData["PotentialTenantId"] = new SelectList(_context.PotentialTenant, "Id", "Email", message.PotentialTenantId);
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManager, "Id", "Email", message.PropertyManagerId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.PotentialTenant)
                .Include(m => m.PropertyManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Message.FindAsync(id);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.Id == id);
        }
    }
}
