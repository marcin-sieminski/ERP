using System;

namespace Firma.Models.EntitiesForView
{
    public class FakturaForAllView
    {
        #region Properties
        public int IdFaktury { get; set; }
        public string Numer { get; set; }
        public DateTime? DataWystawienia { get; set; }
        public string KontrahentNazwa { get; set; }
        public string KontrahentNIP { get; set; }
        public string KontrahentAdres { get; set; }
        public DateTime? TerminPlatnosci { get; set; }
        public string SposobPlatnosciNazwa { get; set; }
        public bool CzyZatwierdzona { get; set; }
        #endregion
    }
}
