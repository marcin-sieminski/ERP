using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class WszystkieFakturyViewModel: WszystkieViewModel<FakturaForAllView>
    {
        #region Konstruktor
        public WszystkieFakturyViewModel() : base("Faktury")
        {

        }
        #endregion

        #region Helpers
        public override void Load()
        {
            AllList = 
                (
                    from faktura in erpEntities.Faktura
                    where faktura.CzyAktywna == true
                    select new FakturaForAllView
                    {
                        IdFaktury = faktura.IdFaktury,
                        Numer = faktura.Numer,
                        DataWystawienia = faktura.DataWystawienia,
                        KontrahentNazwa = faktura.Kontrahent.Nazwa,
                        KontrahentNIP = faktura.Kontrahent.Nip,
                        KontrahentAdres = 
                            faktura.Kontrahent.KodPocztowy + " " +
                            faktura.Kontrahent.Miasto + " " +
                            faktura.Kontrahent.Ulica,
                        TerminPlatnosci = faktura.TerminPlatnosci,
                        SposobPlatnosciNazwa = faktura.SposobPlatnosci.Nazwa
                    }
                ).ToList();
            Filter();
        }

        protected override void Filter()
        {
            if (string.IsNullOrEmpty(SearchPhrase))
                return;

            switch(SelectedFilter)
            {
                case nameof(Faktura.Numer):
                    List = new ObservableCollection<FakturaForAllView>(AllList
                        .Where(item => item.Numer?.Contains(SearchPhrase) ?? false));
                    break;
                default:
                    List = new ObservableCollection<FakturaForAllView>(AllList);
                    break;
            };

            OrderBy();
        }

        protected override void Open()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            if(SelectedItem != null)
            {
                erpEntities.Faktura.First(item => item.IdFaktury == SelectedItem.IdFaktury).CzyAktywna = false;
                try
                {
                    erpEntities.SaveChanges();
                }
                catch(Exception e)
                {
                    Debug.WriteLine($"Wystąpił błąd usuwania\n{e.Message}");
                }
            }
        }

        protected override void OrderBy()
        {
            if (string.IsNullOrEmpty(SelectedFilter))
                return;

            switch(SelectedOrderBy)
            {
                case nameof(Faktura.Numer):
                    List = new ObservableCollection<FakturaForAllView>(OrderDescending 
                        ? List.OrderByDescending(item => item.Numer)
                        : List.OrderBy(item => item.Numer));
                    break;
            };
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsFilter()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(Faktura.Numer), "Numer"),
            };
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsOrderBy()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(Faktura.Numer), "Numer"),
            };
        }
        #endregion
    }
}
