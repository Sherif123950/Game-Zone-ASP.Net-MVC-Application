using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly ApplicationDbContext _dbContext;

		public GenericRepository(ApplicationDbContext dbContext)
        {
			this._dbContext = dbContext;
		}
        public IEnumerable<SelectListItem> GetSelectedList()
		{
			return _dbContext.Set<T>()
				.Select(D => new SelectListItem() { Value = D.Id.ToString(), Text = D.Name })
				.OrderBy(SL => SL.Text).AsNoTracking().ToList();
		}
	}
}
