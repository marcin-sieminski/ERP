using Firma.ViewResources;

namespace Firma.Models.Validators
{
    public class BusinessValidator : Validator
    {
        public static string SprawdzRabat(decimal? rabat)
        {
            return rabat >= 0 && rabat < 100 ? string.Empty : BaseResources.InvalidRabat;
        }
    }
}
