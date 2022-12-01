using System;
using System.Globalization;
using System.Web.Mvc;
namespace MIMS.Util
{
    public class DateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueResult != null)
            {
                // Query string value provider uses Invariant culture, hence datetime is not converted correctly in our case.
                // More info http://stackoverflow.com/questions/528545/mvc-datetime-binding-with-incorrect-date-format

                return valueResult.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);
            }

            if (bindingContext.ModelType.IsGenericType && bindingContext.ModelType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                return null;
            }

            return default(DateTime);
        }
    }
}