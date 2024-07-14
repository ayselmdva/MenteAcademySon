using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationBD.DAL;
using WebApplicationBD.Models;

namespace WebApplicationBD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProfessionController : Controller
    {
        private readonly AppDbContext _context;

        public ProfessionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Professions.ToListAsync());
        }

        [HttpGet]
        public async Task <IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(Profession profession)
        {
            if (ModelState.IsValid) { return View(); }
            if (profession == null) { ModelState.AddModelError("", "NotFound"); return View(); }
            await _context.Professions.AddAsync(profession);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Profession? profession= await _context.Professions.FirstOrDefaultAsync(p => p.Id == id);
            //if (profession != null) { NotFound();  return View(); }
            return View(profession);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Profession profession)
        {
            if (profession == null) {  return View(); }
            Profession ? exists = await _context.Professions.FirstOrDefaultAsync(x=>x.Id == profession.Id);
           // if (exists != null) { NotFound(); return View(); }
            exists.Name= profession.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult>Delete(int id)
        {
            Profession?  profession = await _context.Professions.FirstOrDefaultAsync(p=>p.Id == id);
            if (profession == null) { NotFound(); return View(); }
             _context.Remove(profession);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
    }
}
