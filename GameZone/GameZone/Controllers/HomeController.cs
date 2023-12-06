using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using GameZone.Models;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IGameRepository _gameRepository;
		private readonly IMapper _mapper;

		public HomeController(ILogger<HomeController> logger,IGameRepository gameRepository,IMapper mapper)
        {
            _logger = logger;
			this._gameRepository = gameRepository;
			this._mapper = mapper;
		}
        public async Task<IActionResult> Index()
        {
            var Games =await _gameRepository.GetAllGamesAsync();
            //var MappedGames = _mapper.Map<IEnumerable<Game>, IEnumerable<CreateGameFormViewmodel>>(Games);
            return View(Games);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
