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
    public class HeroesController : Controller
    {
        private readonly OverwatchContext _context;

        public HeroesController(OverwatchContext context)
        {
            _context = context;
        }

        // GET: Heroes
        public async Task<IActionResult> Index(string searchString, string selectedRole, string sortOrder)
        {
            ViewData["CurrentSearch"] = searchString;
            ViewData["CurrentRole"] = selectedRole;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DifficultySortParm"] = sortOrder == "difficulty" ? "difficulty_desc" : "difficulty";

            var heroes = from h in _context.Heroes select h;

            // Search by Name
            if (!string.IsNullOrEmpty(searchString))
            {
                heroes = heroes.Where(h => h.Name.Contains(searchString));
            }

            // Filter by Role (Enum comparison instead of string comparison)
            if (!string.IsNullOrEmpty(selectedRole))
            {
                if (Enum.TryParse<RoleType>(selectedRole, out var roleEnum))
                {
                    heroes = heroes.Where(h => h.Role == roleEnum);
                }
            }

            // Sorting
            heroes = sortOrder switch
            {
                "name_desc" => heroes.OrderByDescending(h => h.Name),
                "difficulty" => heroes.OrderBy(h => h.DifficultyToMaster),
                "difficulty_desc" => heroes.OrderByDescending(h => h.DifficultyToMaster),
                _ => heroes.OrderBy(h => h.Name)
            };

            // Populate dropdown list in View
            ViewBag.RoleList = Enum.GetNames(typeof(RoleType)).ToList();

            return View(await heroes.AsNoTracking().ToListAsync());
        }

        // GET: Heroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hero = await _context.Heroes
                .FirstOrDefaultAsync(m => m.HeroID == id);
            if (hero == null)
            {
                return NotFound();
            }

            return View(hero);
        }

        // GET: Heroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Heroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hero hero, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                hero.ImageURL = "/images/" + fileName;
            }

            if (ModelState.IsValid)
            {
                _context.Add(hero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(hero);
        }


        // GET: Heroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
                return NotFound();

            return View(hero);
        }

        // POST: Heroes/Edit/5
        // POST: Heroes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Hero hero, IFormFile ImageFile)
        {
            if (id != hero.HeroID)
                return NotFound();

            // Load the existing hero from DB (tracked entity)
            var existingHero = await _context.Heroes.FindAsync(id);
            if (existingHero == null)
                return NotFound();

            // Copy editable scalar properties from posted 'hero' to the tracked entity.
            // Do NOT overwrite ImageURL here (we'll only change it if a new file is uploaded).
            existingHero.Name = hero.Name;
            existingHero.Role = hero.Role;
            existingHero.Description = hero.Description;
            existingHero.CountryOfOrigin = hero.CountryOfOrigin;
            existingHero.Gender = hero.Gender;
            existingHero.WeaponType = hero.WeaponType;
            existingHero.DifficultyToMaster = hero.DifficultyToMaster;
            // If you have collections (Abilities), handle them separately if needed.

            // Handle optional image upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                existingHero.ImageURL = "/images/" + fileName;
            }
            // else: keep existingHero.ImageURL unchanged

            // Clear ModelState and revalidate the tracked entity so the validator checks the current state.
            ModelState.Clear();
            if (!TryValidateModel(existingHero))
            {
                // Validation failed — return view with the tracked entity so validation messages show.
                return View(existingHero);
            }

            try
            {
                // Save changes for the tracked entity
                _context.Update(existingHero);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Heroes.Any(e => e.HeroID == existingHero.HeroID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Heroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hero = await _context.Heroes
                .FirstOrDefaultAsync(m => m.HeroID == id);
            if (hero == null)
            {
                return NotFound();
            }

            return View(hero);
        }

        // POST: Heroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero != null)
            {
                _context.Heroes.Remove(hero);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeroExists(int id)
        {
            return _context.Heroes.Any(e => e.HeroID == id);
        }
    }
}
