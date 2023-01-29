using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;

namespace Firma.ViewModels
{
    public class NowaPozycjaFakturyViewModel : JedenViewModel<PozycjaFaktury>, IDataErrorInfo
    {
        #region PolaIWlasciwosci

        public List<ComboBoxKeyAndValue> Towary { get; set; }

        public int? IdTowaru
        {
            get
            {
                return Item.IdTowar;
            }
            set
            {
                if (Item.IdTowar != value)
                {
                    Item.IdTowar = value.Value;
                    OnPropertyChanged(() => IdTowaru);
                }
            }
        }

        public decimal? Ilosc
        {
            get
            {
                return Item.Ilosc;
            }
            set
            {
                if (Item.Ilosc != value)
                {
                    Item.Ilosc = value;
                    OnPropertyChanged(() => Ilosc);
                }
            }
        }

        public decimal? Cena
        {
            get
            {
                return Item.Cena;
            }
            set
            {
                if (Item.Cena != value)
                {
                    Item.Cena = value;
                    OnPropertyChanged(() => Cena);
                }
            }
        }

        public decimal? Rabat
        {
            get
            {
                return Item.Rabat;
            }
            set
            {
                if (Item.Rabat != value)
                {
                    Item.Rabat = value;
                    OnPropertyChanged(() => Rabat);
                }
            }
        }

        #endregion



        #region Konstruktory

        public NowaPozycjaFakturyViewModel() : base("Pozycja faktury")
        {
            Item = new PozycjaFaktury()
            {
                CzyAktywny = true,
                Cena = 0,
                Rabat = 0,
                Ilosc = 0
            };

            Towary = Db.Towar.Where(item => item.IsActive == true)
                             .Select(item => new ComboBoxKeyAndValue() { Key = item.Id, Value = item.Nazwa })
                             .ToList();

            IdTowaru = Towary.FirstOrDefault()?.Key;
        }

        #endregion


        #region Metody

        protected override void Save()
        {
            Messenger.Default.Send(Item);
        }

        #endregion


        #region Walidacja

        protected override bool IsValid()
        {
            //można pozbierać te błędy i wyświetlić w podsumowaniu komunikat
            return 
                this[nameof(Rabat)] == string.Empty &&
                this[nameof(Ilosc)] == string.Empty;
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case (nameof(Rabat)):
                        return BusinessValidator.SprawdzRabat(Rabat ?? 0);
                    case (nameof(Ilosc)):
                        return DecimalValidator.CzyNieUjemne(Ilosc ?? -1);
                }
                return string.Empty;
            }
        }

        #endregion
    }

}



