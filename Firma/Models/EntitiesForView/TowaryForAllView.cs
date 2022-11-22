namespace Firma.Models.EntitiesForView
{
    public class TowaryForAllView
    {
        #region Properties
        public int Id { get; set; }
        public string KodCN { get; set; }
        public string NumerKatalogowy { get; set; }
        public string Nazwa { get; set; }
        public string URL { get; set; }
        public string KategoriaKod { get; set; }
        public string Opis { get; set; }
        public string JM { get; set; }
        public string Waluta { get; set; }
        public decimal Cena { get; set; }
        public string NazwaKontrahenta { get; set; }
        public string NazwaProducenta { get; set; }
        public decimal Waga { get; set; }
        public string OpiekunImieNazwisko { get; set; }

        #endregion
    }
}
