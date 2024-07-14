using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationBD.DAL;
using WebApplicationBD.Models;
using WebApplicationBD.ViewModels;

namespace WebApplicationBD.Controllers
{
    public class HomeController:Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM()
            {
                Teams = await _appDbContext.Teams.Include(x => x.Profession).Take(4).ToListAsync()
            };
            return View(vm);
        }
    }
}
