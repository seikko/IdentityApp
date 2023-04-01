using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IdentityApp.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddModelErrorList(this ModelStateDictionary modelState,List<string> errors)
        {
            foreach (var item in errors)
            {
                modelState.AddModelError(string.Empty, item);
            }
        }
    }
}
