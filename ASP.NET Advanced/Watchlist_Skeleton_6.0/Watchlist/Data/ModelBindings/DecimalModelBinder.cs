﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Watchlist.Data.ModelBindings
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);
            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
            {
                decimal actualValue = 0M;
                bool success = false;
                try
                {
                    string decValue = valueResult.FirstValue;
                    decValue = decValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    decValue = decValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    actualValue = Convert.ToDecimal(decValue, CultureInfo.CurrentCulture);
                    success = true;

                    if (success)
                    {
                        bindingContext.Result = ModelBindingResult.Success(actualValue);
                    }
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);

                }

            }
            return Task.CompletedTask;
        }
    }
}
