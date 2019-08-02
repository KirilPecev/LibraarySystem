namespace Libraary.Web.CustomAttributes
{
    using Models.Books;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ImageAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (BookInputModel) validationContext.ObjectInstance;

            if(string.Equals(model.Picture.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(model.Picture.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(model.Picture.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(model.Picture.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(model.Picture.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage()
        {
            return $"Invalid image format! Please upload image with one of this formats: jpg, jpeg, pjpeg, x-png, png.";
        }
    }
}
