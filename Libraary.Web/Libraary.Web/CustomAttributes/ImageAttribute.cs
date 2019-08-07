namespace Libraary.Web.CustomAttributes
{
    using Models.Books;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ImageAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            try
            {
                var model = (BookInputModel)validationContext.ObjectInstance;

                if (Check(model.Picture.ContentType))
                {
                    return ValidationResult.Success;
                }
            }
            catch (Exception e)
            {
                var model = (BookEditInputModel)validationContext.ObjectInstance;

                if (model.NewPicture == null)
                {
                    return ValidationResult.Success;
                }

                if (Check(model.NewPicture.ContentType))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(GetErrorMessage());
        }

        private bool Check(string contentType)
        {
            if (string.Equals(contentType, "image/jpg", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(contentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(contentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(contentType, "image/x-png", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(contentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private string GetErrorMessage()
        {
            return $"Invalid image format! Please upload image with one of this formats: jpg, jpeg, pjpeg, x-png, png.";
        }
    }
}
