using DataAccessLayer.Entities;
using GameZone.Helpers;
using GameZone.Helpers.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels
{
	public class CreateGameFormViewmodel: GameFormViewModel
    {
		[AllowedExtension(DocumentationSettings.AllowedExtensions)]
		[MaximumFileSize(DocumentationSettings.FileMaximumSizeInByte)]
		public IFormFile Cover { get; set; } = default!;
		
	}
}
