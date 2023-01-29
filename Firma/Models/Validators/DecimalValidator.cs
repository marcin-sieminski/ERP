using Firma.ViewResources;

namespace Firma.Models.Validators
{
    public class DecimalValidator : Validator
    {
        /// <summary>
        /// Sprawdza czy wartość nie jest ujemna
        /// </summary>
        /// <param name="value">wartość do sprawdzenia</param>
        /// <returns></returns>
        public static string CzyNieUjemne(decimal value)
        {
            return value >= 0 ? string.Empty : BaseResources.ThisValueHasToBePositiv;
        }
    }
}
