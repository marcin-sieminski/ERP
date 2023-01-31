using System;
using Firma.ViewResources;

namespace Firma.Models.Validators
{
    public class PracownikValidator : Validator
    {
        public static string SprawdzCzyPuste(string napis)
        {
            if (string.IsNullOrEmpty(napis))
            {
                return BaseResources.FieldIsRequired;
            }
            return string.Empty;
        }

        public static string SprawdzNip(string nip)
        {
            if (nip is null)
            {
                return BaseResources.FieldIsRequired;
            }
            return nip.Length == 10 ? string.Empty : BaseResources.InvalidNip;
        }

        public static string SprawdzPesel(string pesel)
        {
            if (pesel is null)
            {
                return BaseResources.FieldIsRequired;
            }
            return pesel.Length == 11 ? string.Empty : BaseResources.InvalidPesel;
        }
    }
}
