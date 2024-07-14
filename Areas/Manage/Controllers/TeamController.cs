using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationBD.DAL;
using WebApplicationBD.Models;
using static NuGet.Packaging.PackagingConstants;

namespace WebApplicationBD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TeamController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.Include(x=>x.Profession).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Professions = await _context.Professions.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            ViewBag.Professions = await _context.Professions.ToListAsync();
            if (!ModelState.IsValid) { return View(); }
            if (team == null) { ModelState.AddModelError("", "NotFound"); return View(); }
            if (!team.formFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("", "");
            }
            if (team.formFile.Length/1024>=200)
            {
                ModelState.AddModelError("", "");
            }
    
            string uniqeName=Guid.NewGuid().ToString() + "_" + team.formFile.FileName;
            string path = $@"{_environment.WebRootPath}\assets\img\team\{uniqeName}";

            FileStream stream = new FileStream(path, FileMode.Create);
            team.formFile.CopyTo(stream);

            team.Image = uniqeName;

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Professions = await _context.Professions.ToListAsync();
            Team? team = await _context.Teams.FirstOrDefaultAsync(p => p.Id == id);
            if (team != null) { NotFound();  return View(); }
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Team team)
        {
            ViewBag.Professions = await _context.Professions.ToListAsync();
            if (team == null) { return View(); }
            Team? exists = await _context.Teams.FirstOrDefaultAsync(x => x.Id == team.Id);
            if (exists != null) { NotFound(); return View(); }
            exists.Name = team.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Team? team = await _context.Teams.FirstOrDefaultAsync(p => p.Id == id);
            if (team == null) { NotFound(); return View(); }
            _context.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
