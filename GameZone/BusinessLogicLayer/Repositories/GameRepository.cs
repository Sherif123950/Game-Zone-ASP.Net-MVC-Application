using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
	public class GameRepository : IGameRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public GameRepository(ApplicationDbContext dbContext)
        {
			this._dbContext = dbContext;
		}
        public async Task<int> AddGame(Game game)
		{
			_dbContext.Games.Add(game);
			return await _dbContext.SaveChangesAsync();
		}

		public async Task<int> DeleteGame(Game game)
		{
			_dbContext.Games.Remove(game);
			return await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Game>> GetAllGames()
		{
			return await _dbContext.Games.ToListAsync();
		}

		public async Task<Game?> GetById(int Id)
		{
			return await _dbContext.Games.FindAsync(Id);
		}

		public async Task<int> UpdateGame(Game game)
		{
			_dbContext.Games.Update(game);
			return await _dbContext.SaveChangesAsync();
		}
	}
}
