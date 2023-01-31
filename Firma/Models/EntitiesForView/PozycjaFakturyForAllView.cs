namespace Firma.Models.EntitiesForView
{
    public class PozycjaFakturyForAllView
    {
        public string TowarKod { get; set; }
        public string TowarNazwa { get; set; }
        public decimal? Cena { get; set; }
        public decimal? Ilosc { get; set; }
        public decimal? Rabat { get; set; }

        public PozycjaFakturyForAllView()
        {
            
        }

        public PozycjaFakturyForAllView(string towarKod, string towarNazwa, decimal? cena, decimal? ilosc, decimal? rabat)
        {
            TowarKod = towarKod;
            TowarNazwa = towarNazwa;
            Cena = cena;
            Ilosc = ilosc;
            Rabat = rabat;
        }
    }
}
