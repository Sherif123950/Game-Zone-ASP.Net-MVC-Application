using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public CategoryRepository(ApplicationDbContext dbContext)
        {
			this._dbContext = dbContext;
		}
        public IEnumerable<SelectListItem> GetSelectedList()
		{
			return _dbContext.Categories
				.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name })
				.OrderBy(SL => SL.Text).AsNoTracking().ToList();
		}
	}
}
