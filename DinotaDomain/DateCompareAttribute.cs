using System;
using System.ComponentModel.DataAnnotations;

namespace Dinota.Domain
{
    [AttributeUsage(AttributeTargets.Property , AllowMultiple = false)]
    public sealed class DateCompareAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "{0} cannot be the same as {1}.";

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public DateCompareAttribute(string startDate,string endDate)
            : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(startDate))
            {
                throw new ArgumentNullException("StartDate");
            }

            if (string.IsNullOrEmpty(endDate))
            {
                throw new ArgumentNullException("EndDate");
            }

            StartDate = startDate;
            EndDate = endDate;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, StartDate);
        }

        protected override ValidationResult IsValid(object value,
                                                ValidationContext validationContext)
        {
            if (value != null)
            {
                var startDate = validationContext.ObjectInstance.GetType().GetProperty(StartDate);
                var endDate = validationContext.ObjectInstance.GetType().GetProperty(EndDate);

                var startDateValue = startDate.GetValue(validationContext.ObjectInstance, null);
                var endDateValue = endDate.GetValue(validationContext.ObjectInstance, null);

                if (DateTime.Parse(startDateValue.ToString()) > DateTime.Parse(endDateValue.ToString()) || DateTime.Parse(startDateValue.ToString()) < DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    return new ValidationResult(
                        FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }

        //public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
        //                                            ModelMetadata metadata,
        //                                            ControllerContext context)
        //{
        //    var clientValidationRule = new ModelClientValidationRule()
        //    {
        //        ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
        //        ValidationType = "CompareStarDateEndDate"
        //    };

        //    clientValidationRule.ValidationParameters.Add("startDate", StartDate);
        //    clientValidationRule.ValidationParameters.Add("endDate", EndDate);

        //    return new[] { clientValidationRule };
        //}
    }
}
