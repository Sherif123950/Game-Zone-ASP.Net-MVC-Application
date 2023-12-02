using DataAccessLayer.Data.Contexts;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
		private readonly ApplicationDbContext _dbContext;

		public GamesController(ApplicationDbContext DbContext)
        {
			_dbContext = DbContext;
		}
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create() 
        {
            CreateGameFormViewmodel ViewModel = new()
            {
                Categories = _dbContext.Categories.Select(c => new SelectListItem(){Value=c.Id.ToString() ,Text=c.Name}).OrderBy(SL=>SL.Text).ToList(),
				Devices = _dbContext.Devices.Select(D => new SelectListItem() { Value = D.Id.ToString(), Text = D.Name }).OrderBy(SL => SL.Text).ToList()

			};
            return View(ViewModel);
        }
    }
}
