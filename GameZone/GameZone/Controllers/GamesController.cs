using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
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
		private readonly IGenericRepository<Device> _deviceRepo;
		private readonly IGenericRepository<Category> _categoryRepo;
		private readonly ApplicationDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IGameRepository _gameRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IDeviceRepository _deviceRepository;

		public GamesController(IGenericRepository<Device> DeviceRepo, IGenericRepository<Category> CategoryRepo, ApplicationDbContext dbContext, IMapper mapper, IGameRepository gameRepository, ICategoryRepository categoryRepository, IDeviceRepository deviceRepository)
		{
			_deviceRepo = DeviceRepo;
			_categoryRepo = CategoryRepo;
			this._dbContext = dbContext;
			this._mapper = mapper;
			this._gameRepository = gameRepository;
			this._categoryRepository = categoryRepository;
			this._deviceRepository = deviceRepository;
		}

		#region Create
		[HttpGet]
		public IActionResult Create()
		{
			CreateGameFormViewmodel ViewModel = new()
			{
				Categories =_categoryRepo.GetSelectedList(),
				Devices = _deviceRepo.GetSelectedList()

			};
			return View(ViewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateGameFormViewmodel GameVM)
		{
			if (!ModelState.IsValid)
			{
				GameVM.Categories = _categoryRepo.GetSelectedList();
				GameVM.Devices = _deviceRepo.GetSelectedList();
				return View(GameVM);
			}
			//upload coevr image to server
			GameVM.CoverName = await DocumentationSettings.UplaodImage(GameVM.Cover, "Games");
			var MappedGame = _mapper.Map<CreateGameFormViewmodel, Game>(GameVM);
			//save game into database
			await _gameRepository.AddGameAsync(MappedGame);
			return RedirectToAction(nameof(Index));
		}
		#endregion

		#region Read 
		public async Task<IActionResult> Index()
		{
			var Games = await _gameRepository.GetAllGamesAsync();
			return View(Games);
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
		#endregion

		#region Update 
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var Game = await _gameRepository.GetByIdAsync(id);
			if (Game is null)
				return NotFound();
			var MappedGame = _mapper.Map<Game, EditFormViewModel>(Game);
			MappedGame.Categories = _categoryRepo.GetSelectedList();
			MappedGame.Devices = _deviceRepo.GetSelectedList();
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
				var Game = await _gameRepository.GetByIdAsync(model.Id);
				if (Game is null)
					return NotFound();
				var OldCoverName = Game.CoverName;
				//-------------------------------------------Soluiton Number 1
				//if (model.Cover is not null)
				//	model.CoverName = await DocumentationSettings.UplaodImage(model.Cover, "Games");
				//var MappedGame = _mapper.Map<EditFormViewModel, Game>(model);
				//var Res = _gameRepository.UpdateGame(model.Id,MappedGame);
				//-------------------------------------------Solution Number 2
				Game.Name = model.Name;
				Game.Descripiton = model.Descripiton;
				Game.CategoryId = model.CategoryId;
				Game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();
				if (model.Cover is not null)
					Game.CoverName = await DocumentationSettings.UplaodImage(model.Cover, "Games");
				var Res = _dbContext.SaveChanges();
				//---------------------------------------------
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
		#endregion

		#region Delete
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			var Game = await _gameRepository.GetByIdAsync(id);
			if (Game is null)
				return NotFound();
			var Result = _gameRepository.DeleteGame(Game);
            if (Result>0)
            {
				DocumentationSettings.DeleteImage("Games", Game.CoverName);
				return Ok();
            }
			return BadRequest();
        }
		#endregion
	}
}
