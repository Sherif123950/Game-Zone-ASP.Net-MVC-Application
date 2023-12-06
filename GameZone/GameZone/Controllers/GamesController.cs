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
		private readonly IMapper _mapper;
		private readonly IGameRepository _gameRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IDeviceRepository _deviceRepository;

		public GamesController(IMapper mapper, IGameRepository gameRepository, ICategoryRepository categoryRepository, IDeviceRepository deviceRepository)
		{
			this._mapper = mapper;
			this._gameRepository = gameRepository;
			this._categoryRepository = categoryRepository;
			this._deviceRepository = deviceRepository;
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
			GameVM.CoverName =await DocumentationSettings.UplaodImage(GameVM.Cover ,"Games");
			var MappedGame = _mapper.Map<CreateGameFormViewmodel, Game>(GameVM);
			//save game into database
			await _gameRepository.AddGameAsync(MappedGame);
			return RedirectToAction(nameof(Index));
		}
	}
}
