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
    public class HeroNotesController : Controller
    {
        private readonly OverwatchContext _context;

        public HeroNotesController(OverwatchContext context)
        {
            _context = context;
        }

        // GET: HeroNotes
        public async Task<IActionResult> Index()
        {
            var overwatchContext = _context.HeroNotes.Include(h => h.Hero).Include(h => h.User);
            return View(await overwatchContext.ToListAsync());
        }

        // GET: HeroNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroNote = await _context.HeroNotes
                .Include(h => h.Hero)
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.NoteID == id);
            if (heroNote == null)
            {
                return NotFound();
            }

            return View(heroNote);
        }

        // GET: HeroNotes/Create
        public IActionResult Create()
        {
            ViewData["HeroID"] = new SelectList(_context.Heroes, "HeroID", "HeroID");
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID");
            return View();
        }

        // POST: HeroNotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteID,Content,DateCreated,DateModified,HoursPlayed,HeroID,UserID")] HeroNote heroNote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(heroNote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HeroID"] = new SelectList(_context.Heroes, "HeroID", "HeroID", heroNote.HeroID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", heroNote.UserID);
            return View(heroNote);
        }

        // GET: HeroNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroNote = await _context.HeroNotes.FindAsync(id);
            if (heroNote == null)
            {
                return NotFound();
            }
            ViewData["HeroID"] = new SelectList(_context.Heroes, "HeroID", "HeroID", heroNote.HeroID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", heroNote.UserID);
            return View(heroNote);
        }

        // POST: HeroNotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoteID,Content,DateCreated,DateModified,HoursPlayed,HeroID,UserID")] HeroNote heroNote)
        {
            if (id != heroNote.NoteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heroNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeroNoteExists(heroNote.NoteID))
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
            ViewData["HeroID"] = new SelectList(_context.Heroes, "HeroID", "HeroID", heroNote.HeroID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", heroNote.UserID);
            return View(heroNote);
        }

        // GET: HeroNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroNote = await _context.HeroNotes
                .Include(h => h.Hero)
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.NoteID == id);
            if (heroNote == null)
            {
                return NotFound();
            }

            return View(heroNote);
        }

        // POST: HeroNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var heroNote = await _context.HeroNotes.FindAsync(id);
            if (heroNote != null)
            {
                _context.HeroNotes.Remove(heroNote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeroNoteExists(int id)
        {
            return _context.HeroNotes.Any(e => e.NoteID == id);
        }
    }
}
