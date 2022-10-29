using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Watchlist.Data.ModelBindings
{
    public class DecimalModelBindingProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            if (context.Metadata.ModelType == typeof(Decimal) || context.Metadata.ModelType == typeof(Decimal?))
            {
                return new DecimalModelBinder();
            }
            return null;
        }
    }
}
