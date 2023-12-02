using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels
{
	public class CreateGameFormViewmodel
	{
		[MaxLength(500)]
		public string Name { get; set; } = string.Empty;
		[Display(Name ="Category")]
		public int CategoryId { get; set; }
		public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
		[Display(Name ="Supported Devices")]
		public List<int> SelectedDevices { get; set; } = new List<int>();
		public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
		[MaxLength(2500)]
		[Display(Name = "Description")]
		public string Descripiton { get; set; } = string.Empty;
		public IFormFile Cover { get; set; } = default!;

	}
}
