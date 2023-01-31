using System;
using Firma.ViewResources;

namespace Firma.Models.Validators
{
    public class KodTowarValidator : Validator
    {
        public static string SprawdzKodTowaru(string kod)
        {
            if (kod is null)
            {
                return BaseResources.FieldIsRequired;
            }
            return kod.Length >= 2 && kod.Length <=4  ? string.Empty : BaseResources.InvalidCode;
        }
    }
}
