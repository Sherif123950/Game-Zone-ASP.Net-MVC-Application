namespace GameZone.Helpers
{
	public static class DocumentationSettings
	{
		public async static Task<string> UplaodImage(IFormFile file, string FolderName)
		{
			var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images", FolderName);
			var ImageName = $"{Guid.NewGuid()}{file.FileName}";
			var ImagePath=Path.Combine(FolderPath, ImageName);
			using var Stream = File.Create(ImagePath);
			await file.CopyToAsync(Stream);
			return ImageName;
		}
	}
}
