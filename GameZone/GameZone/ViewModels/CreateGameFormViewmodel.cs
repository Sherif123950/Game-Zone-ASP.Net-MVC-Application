using DataAccessLayer.Entities;
using GameZone.Helpers;
using GameZone.Helpers.Attributes;
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
		public List<int> SelectedDevices { get; set; } = default!;
		public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
		[MaxLength(2500)]
		[Display(Name = "Description")]
		public string Descripiton { get; set; } = string.Empty;
		[AllowedExtension(DocumentationSettings.AllowedExtensions)]
		[MaximumFileSize(DocumentationSettings.FileMaximumSizeInByte)]
		public IFormFile Cover { get; set; } = default!;
		public string CoverName { get; set; }= string.Empty;
	}
}
