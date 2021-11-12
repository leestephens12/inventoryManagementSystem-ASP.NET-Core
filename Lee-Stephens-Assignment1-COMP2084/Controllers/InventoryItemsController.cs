using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lee_Stephens_Assignment1_COMP2084.Data;
using Lee_Stephens_Assignment1_COMP2084.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lee_Stephens_Assignment1_COMP2084.Controllers
{
    public class InventoryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InventoryItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InventoryItems.Include(i => i.Item);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InventoryItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItems
                .Include(i => i.Item)
                .FirstOrDefaultAsync(m => m.InventoryItemId == id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return View(inventoryItem);
        }

        // GET: InventoryItems/Create
        //Only Authorized users under role administrator can access
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["ItemName"] = new SelectList(_context.Items.OrderBy(c => c.ItemName), "ItemName", "ItemName");
            ViewData["ItemId"] = new SelectList(_context.Items.OrderBy(c => c.Section), "ItemId", "Section");
            return View();
        }

        // POST: InventoryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventoryItemId,ItemName,Quantity,InStock,StoreLocation,ItemId")] InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemName"] = new SelectList(_context.Items.OrderBy(c => c.ItemName), "ItemName", "ItemName", inventoryItem.ItemName);
            ViewData["ItemId"] = new SelectList(_context.Items.OrderBy(c => c.Section), "ItemId", "Section", inventoryItem.ItemId);
            return View(inventoryItem);
        }

        // GET: InventoryItems/Edit/5
        //Only Authorized users under role administrator can access
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }
            ViewData["ItemName"] = new SelectList(_context.Items.OrderBy(c => c.ItemName), "ItemName", "ItemName", inventoryItem.ItemName);
            ViewData["ItemId"] = new SelectList(_context.Items.OrderBy(c => c.Section), "ItemId", "Section", inventoryItem.ItemId);
            return View(inventoryItem);
        }

        // POST: InventoryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventoryItemId,ItemName,Quantity,InStock,StoreLocation,ItemId")] InventoryItem inventoryItem)
        {
            if (id != inventoryItem.InventoryItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryItemExists(inventoryItem.InventoryItemId))
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
            ViewData["ItemName"] = new SelectList(_context.Items.OrderBy(c => c.ItemName), "ItemName", "ItemName", inventoryItem.ItemName);
            ViewData["ItemId"] = new SelectList(_context.Items.OrderBy(c => c.Section), "ItemId", "Section", inventoryItem.ItemId);
            return View(inventoryItem);
        }

        // GET: InventoryItems/Delete/5
        //Only Authorized users under role administrator can access
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItems
                .Include(i => i.Item)
                .FirstOrDefaultAsync(m => m.InventoryItemId == id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return View(inventoryItem);
        }

        // POST: InventoryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            _context.InventoryItems.Remove(inventoryItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryItemExists(int id)
        {
            return _context.InventoryItems.Any(e => e.InventoryItemId == id);
        }
    }
}
