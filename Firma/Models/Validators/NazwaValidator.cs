using System;
using Firma.ViewResources;

namespace Firma.Models.Validators
{
    public class NazwaValidator : Validator
    {
        public static string SprawdzNazwe(string nazwa)
        {
            if (nazwa is null)
            {
                return BaseResources.FieldIsRequired;
            }
            return nazwa.Length >= 2 ? string.Empty : BaseResources.InvalidName;
        }
    }
}
