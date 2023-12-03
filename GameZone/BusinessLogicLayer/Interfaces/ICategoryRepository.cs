using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusinessLogicLayer.Interfaces
{
	public interface ICategoryRepository
	{
		IEnumerable<SelectListItem> GetSelectedList();
	}
}
