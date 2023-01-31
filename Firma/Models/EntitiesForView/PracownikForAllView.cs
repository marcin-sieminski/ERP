using System;

namespace Firma.Models.EntitiesForView
{
    public class PracownikForAllView
    {
        public int Id { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        public DateTime? ZatrudnionyOd { get; set; }
        public DateTime? ZatrudnionyDo { get; set; }
        public string Kraj { get; set; }
        public string Miasto { get; set; }
        public string Wojewodztwo { get; set; }
        public string Gmina { get; set; }
        public string Wyksztalcenie { get; set; }
        public string StanCywilny { get; set; }
        public string NrRachunku { get; set; }
        public string DowodOsobisty { get; set; }
    }
}
