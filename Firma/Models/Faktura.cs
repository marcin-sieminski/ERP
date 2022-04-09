namespace Firma.Models
{
    public class Faktura
    {
        public int Numer { get; set; }
        public double Vat { get; set; }
        public bool Oplacona { get; set; }

        public Faktura()
        {
            Numer = 0;
            Vat = 0.23;
            Oplacona = true;
        }
    }
}
