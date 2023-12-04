using System.ComponentModel.DataAnnotations;

namespace GameZone.Helpers.Attributes
{
	public class AllowedExtensionAttribute:ValidationAttribute
	{
		private readonly string _allowedExtensions;

		public AllowedExtensionAttribute(string AllowedExtensions)
        {
			_allowedExtensions = AllowedExtensions;
		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var File = value as IFormFile;
			if (File != null) 
			{
				var FileExtension = Path.GetExtension(File.FileName);
				var IsAllowed = _allowedExtensions.Split(",").Contains(FileExtension, StringComparer.OrdinalIgnoreCase);
				if (!IsAllowed) 
				{
					return new ValidationResult($"only {_allowedExtensions} Is Allowed To Use!");
				}
			}
			return ValidationResult.Success;
		}
	}
}
