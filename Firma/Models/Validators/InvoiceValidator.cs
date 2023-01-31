using System;
using Firma.ViewResources;

namespace Firma.Models.Validators
{
    public class InvoiceValidator : Validator
    {
        public static string SprawdzNumerFaktury(string numerFaktury)
        {
            if (numerFaktury is null)
            {
                return BaseResources.FieldIsRequired;
            }
            return numerFaktury.EndsWith(DateTime.Today.Year.ToString()) ? string.Empty : BaseResources.InvalidInvoiceNumber;
        }
    }
}
