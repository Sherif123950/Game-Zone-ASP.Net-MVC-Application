namespace GameZone.Helpers
{
	public static class DocumentationSettings
	{
		public  const string AllowedExtensions = ".jpg,.jpeg,.png";
		public  const int FileMaximumSizeInMB = 1;
		public  const int FileMaximumSizeInByte = 1*1024*1024;
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
