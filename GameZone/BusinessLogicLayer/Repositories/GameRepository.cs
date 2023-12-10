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
		public async Task<int> AddGameAsync(Game game)
		{
			_dbContext.Games.Add(game);
			return await _dbContext.SaveChangesAsync();
		}

		public int DeleteGame(Game game)
		{
			_dbContext.Games.Remove(game);
			return _dbContext.SaveChanges();
		}

		public async Task<IEnumerable<Game>> GetAllGamesAsync()
		{
			return await _dbContext.Games.Include(G => G.Category).Include(G => G.Devices).ThenInclude(D => D.Device).AsNoTracking().ToListAsync();
		}

		public async Task<Game?> GetByIdAsync(int Id)
		{
			return await _dbContext.Games.Include(G => G.Category).Include(G => G.Devices)
				.ThenInclude(D => D.Device).SingleOrDefaultAsync(G => G.Id == Id);
			//return await _dbContext.Games.FindAsync(Id);
		}

		public int UpdateGame(Game game)
		{
			_dbContext.Games.Update(game);
			return _dbContext.SaveChanges();
		}
	}
}
