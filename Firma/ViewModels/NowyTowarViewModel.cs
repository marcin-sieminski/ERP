using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels
{
    public class NowyTowarViewModel : JedenViewModel<Towar>
    {
        #region Konstruktor
        public NowyTowarViewModel() : base("Towar")
        {
            Item = new Towar();
        }
        #endregion

        #region Properties
        public int Kod 
        {
            get => Item.Kod.Value;
            set
            {
                if (value != Item.Kod)
                    Item.Kod = value;
                base.OnPropertyChanged(() => Kod);
            }
        }

        public string Nazwa
        {
            get => Item.Nazwa;
            set
            {
                if (value != Item.Nazwa)
                    Item.Nazwa = value;
                base.OnPropertyChanged(() => Nazwa);
            }
        }

        public decimal? StawkaVatSprzedazy
        {
            get => Item.StawkaVatSprzedazy;
            set
            {
                if (value != Item.StawkaVatSprzedazy)
                    Item.StawkaVatSprzedazy = value.Value;
                base.OnPropertyChanged(() => StawkaVatSprzedazy);
            }
        }

        public decimal? StawkaVatZakupu
        {
            get => Item.StawkaVatZakupu;
            set
            {
                if (value != Item.StawkaVatZakupu)
                    Item.StawkaVatZakupu = value;
                base.OnPropertyChanged(() => StawkaVatZakupu);
            }
        }

        public decimal? Cena
        {
            get => Item.Cena;
            set
            {
                if (value != Item.Cena)
                    Item.Cena = value.Value;
                base.OnPropertyChanged(() => Cena);
            }
        }

        public decimal? Marza
        {
            get => Item.Marza;
            set
            {
                if (value != Item.Marza)
                    Item.Marza = value.Value;
                base.OnPropertyChanged(() => Marza);
            }
        }
        #endregion

        #region MyRegion
        protected override void Save()
        {
            Item.IsActive = true;
            Db.Towar.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
