using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Interfaces
{
	public interface IGameRepository
	{
		Task<IEnumerable<Game>> GetAllGamesAsync();
		Task<Game?> GetByIdAsync(int Id);
		Task<int> AddGameAsync(Game game);
		//int UpdateGame(int id, Game game);
		int DeleteGame(Game game);
	}
}
