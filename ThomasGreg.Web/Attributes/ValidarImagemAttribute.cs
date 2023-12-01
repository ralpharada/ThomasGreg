using System.ComponentModel.DataAnnotations;

namespace ThomasGreg.Web.Attributes
{
    public class ValidarImagemAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                return ValidationResult.Success;
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" }; // Adicione as extensões permitidas

            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return new ValidationResult("Somente imagens são permitidas.");
            }

            return ValidationResult.Success;
        }
    }
}
