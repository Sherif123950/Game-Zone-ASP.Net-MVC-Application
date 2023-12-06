using System.ComponentModel.DataAnnotations;

namespace GameZone.Helpers.Attributes
{
	public class MaximumFileSizeAttribute : ValidationAttribute
	{
		private readonly int _maxFileSize;
		public MaximumFileSizeAttribute(int MaxFileSize)
        {
			_maxFileSize = MaxFileSize;
		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var File = value as IFormFile;
			if (File != null) 
                if ((File.Length > _maxFileSize))
					return new ValidationResult($"Maximum File Size Is {_maxFileSize} byte");
			return ValidationResult.Success;
		}
	}
}
