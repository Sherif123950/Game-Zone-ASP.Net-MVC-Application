using GameZone.Helpers.Attributes;
using GameZone.Helpers;

namespace GameZone.ViewModels
{
    public class EditFormViewModel : GameFormViewModel
    {
        public int Id { get; set; }
        [AllowedExtension(DocumentationSettings.AllowedExtensions)]
        [MaximumFileSize(DocumentationSettings.FileMaximumSizeInByte)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
