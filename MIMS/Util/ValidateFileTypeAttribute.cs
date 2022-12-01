using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlfasiWeb.Util
{
    public class ValidateFileTypeAttribute : ValidationAttribute, IClientValidatable
    {
        private string ValidFileTypes { get; set; }

        /// <summary>
        /// names of valid file types seperated by commas,should remove the dot from the type
        /// </summary>
        /// <param name="validFileTypes"></param>
        public ValidateFileTypeAttribute(string validFileTypes)
        {
            ValidFileTypes = validFileTypes;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }

            var fileType = file.FileName.Substring(file.FileName.LastIndexOf('.')).Trim('.');

            if (!string.IsNullOrEmpty(ValidFileTypes))
            {
                var arrValidFileTypes = ValidFileTypes.Split(',');
                if (arrValidFileTypes.Length > 0 && !arrValidFileTypes.Contains(fileType))
                {
                    return false;
                }
            }
            return false;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return null;
            }

            var fileType = file.FileName.Substring(file.FileName.LastIndexOf('.')).Trim('.');

            if (!string.IsNullOrEmpty(ValidFileTypes))
            {
                var arrValidFileTypes = ValidFileTypes.Split(',');
                if (arrValidFileTypes.Length > 0 && !arrValidFileTypes.Contains(fileType))
                {
                    return new ValidationResult(ErrorMessage ?? "Invalid file type.");
                }

            }
            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var mcvrTwo = new ModelClientValidationRule();
            mcvrTwo.ValidationType = "customvalidatefile";
            mcvrTwo.ErrorMessage = ErrorMessage;
            mcvrTwo.ValidationParameters.Add("validfiletypes", ValidFileTypes);

            return new List<ModelClientValidationRule> { mcvrTwo };
        }
    }
}