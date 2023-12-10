using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
using GameZone.Helpers;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
	public class GamesController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IGameRepository _gameRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IDeviceRepository _deviceRepository;

		public GamesController(ApplicationDbContext dbContext, IMapper mapper, IGameRepository gameRepository, ICategoryRepository categoryRepository, IDeviceRepository deviceRepository)
		{
			this._dbContext = dbContext;
			this._mapper = mapper;
			this._gameRepository = gameRepository;
			this._categoryRepository = categoryRepository;
			this._deviceRepository = deviceRepository;
		}
		public async Task<IActionResult> Index()
		{
			var Games = await _gameRepository.GetAllGamesAsync();
			return View(Games);
		}
		[HttpGet]
		public IActionResult Create()
		{
			CreateGameFormViewmodel ViewModel = new()
			{
				Categories = _categoryRepository.GetSelectedList(),
				Devices = _deviceRepository.GetSelectedList()

			};
			return View(ViewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateGameFormViewmodel GameVM)
		{
			if (!ModelState.IsValid)
			{
				GameVM.Categories = _categoryRepository.GetSelectedList();
				GameVM.Devices = _deviceRepository.GetSelectedList();
				return View(GameVM);
			}
			//upload coevr image to server
			GameVM.CoverName = await DocumentationSettings.UplaodImage(GameVM.Cover, "Games");
			var MappedGame = _mapper.Map<CreateGameFormViewmodel, Game>(GameVM);
			//save game into database
			await _gameRepository.AddGameAsync(MappedGame);
			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			var Game = await _gameRepository.GetByIdAsync(id.Value);
			if (Game == null)
			{
				return NotFound();
			}
			return View(Game);
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var Game = await _gameRepository.GetByIdAsync(id);
			if (Game is null)
				return NotFound();
			var MappedGame = _mapper.Map<Game, EditFormViewModel>(Game);
			MappedGame.Categories = _categoryRepository.GetSelectedList();
			MappedGame.Devices = _deviceRepository.GetSelectedList();
			return View(MappedGame);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] int? id, EditFormViewModel model)
		{
			if (model.Id != id)
			{
				return BadRequest();
			}
			if (ModelState.IsValid)
			{
				var OldCoverName = model.CoverName;				
				var Game = await _gameRepository.GetByIdAsync(model.Id);
				if (Game is null)
					return NotFound();
				//var MappedGame = _mapper.Map<EditFormViewModel, Game>(model);
				//-------------------------------------------
				Game.Name = model.Name;
				Game.Descripiton = model.Descripiton;
				Game.CategoryId = model.CategoryId;
				Game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();
				if (model.Cover is not null)
					Game.CoverName = await DocumentationSettings.UplaodImage(model.Cover, "Games");
				var Res = _dbContext.SaveChanges();
				//---------------------------------------------
				//var Res = _gameRepository.UpdateGame(MappedGame);
				if (Res > 0)
				{
                    if (model.Cover is not null)
                        DocumentationSettings.DeleteImage("Games", OldCoverName);                    
					return RedirectToAction(nameof(Index));
				}
			}
			DocumentationSettings.DeleteImage("Games", model.CoverName);
			return View(model);
		}
	}
}
