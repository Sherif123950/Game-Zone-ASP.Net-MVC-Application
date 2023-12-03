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
		Task<IEnumerable<Game>> GetAllGames();
		Task<Game?> GetById(int Id);
		Task<int> AddGame(Game game);
		Task<int> UpdateGame(Game game);
		Task<int> DeleteGame(Game game);
	}
}
