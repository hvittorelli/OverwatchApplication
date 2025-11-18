using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OverwatchApplication.Models;

namespace OverwatchApplication.Controllers
{
    public class AbilitiesController : Controller
    {
        private readonly OverwatchContext _context;

        public AbilitiesController(OverwatchContext context)
        {
            _context = context;
        }

        // GET: Abilities
        public async Task<IActionResult> Index()
        {
            var overwatchContext = _context.Abilities.Include(a => a.Hero);
            return View(await overwatchContext.ToListAsync());
        }

        // GET: Abilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ability = await _context.Abilities
                .Include(a => a.Hero)
                .FirstOrDefaultAsync(m => m.AbilityID == id);
            if (ability == null)
            {
                return NotFound();
            }

            return View(ability);
        }

        // GET: Abilities/Create
        public IActionResult Create()
        {
            ViewData["HeroID"] = new SelectList(_context.Heroes, "HeroID", "HeroID");
            return View();
        }

        // POST: Abilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AbilityID,Name,Description,Type,Cooldown,HeroID")] Ability ability)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HeroID"] = new SelectList(_context.Heroes, "HeroID", "HeroID", ability.HeroID);
            return View(ability);
        }

        // GET: Abilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ability = await _context.Abilities.FindAsync(id);
            if (ability == null)
            {
                return NotFound();
            }
            ViewData["HeroID"] = new SelectList(_context.Heroes, "HeroID", "HeroID", ability.HeroID);
            return View(ability);
        }

        // POST: Abilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AbilityID,Name,Description,Type,Cooldown,HeroID")] Ability ability)
        {
            if (id != ability.AbilityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbilityExists(ability.AbilityID))
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
            ViewData["HeroID"] = new SelectList(_context.Heroes, "HeroID", "HeroID", ability.HeroID);
            return View(ability);
        }

        // GET: Abilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ability = await _context.Abilities
                .Include(a => a.Hero)
                .FirstOrDefaultAsync(m => m.AbilityID == id);
            if (ability == null)
            {
                return NotFound();
            }

            return View(ability);
        }

        // POST: Abilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ability = await _context.Abilities.FindAsync(id);
            if (ability != null)
            {
                _context.Abilities.Remove(ability);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbilityExists(int id)
        {
            return _context.Abilities.Any(e => e.AbilityID == id);
        }
    }
}
