using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;

namespace Firma.ViewModels
{
    public class WszystkieFakturyViewModel : WszystkieViewModel<FakturaForAllView>
    {
        public bool CzyModyfikowac { get; set; }

        #region Konstruktor
        public WszystkieFakturyViewModel() : base("Faktury")
        {
            CzyModyfikowac = false;
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
                        KontrahentAdres = faktura.Kontrahent.Adres.KodPocztowy + " " + faktura.Kontrahent.Adres.Miejscowosc + " " + faktura.Kontrahent.Adres.Ulica,
                        TerminPlatnosci = faktura.TerminPlatnosci,
                        SposobPlatnosciNazwa = faktura.SposobPlatnosci.Nazwa,
                        CzyZatwierdzona = faktura.CzyZatwierdzona.Value
                    }
                ).ToList();

            Filter();
        }

        protected override void Filter()
        {
            if (!string.IsNullOrEmpty(SearchPhrase))
            {
                switch (SelectedFilter)
                {
                    case nameof(Faktura.Numer):
                        List = new ObservableCollection<FakturaForAllView>(AllList
                            .Where(item => item.Numer?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                        break;
                    case nameof(Faktura.Kontrahent.Nazwa):
                        List = new ObservableCollection<FakturaForAllView>(AllList
                            .Where(item => item.KontrahentNazwa?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                        break;
                    default:
                        List = new ObservableCollection<FakturaForAllView>(AllList);
                        break;
                };
            }
            else
            {
                List = new ObservableCollection<FakturaForAllView>(AllList);
                OrderBy();
            }
        }

        protected override void OrderBy()
        {
            if (!string.IsNullOrEmpty(SelectedOrderBy))
            {
                switch (SelectedOrderBy)
                {
                    case nameof(Faktura.Numer):
                        List = new ObservableCollection<FakturaForAllView>(OrderDescending
                            ? List.OrderByDescending(item => item.Numer)
                            : List.OrderBy(item => item.Numer));
                        break;
                    case nameof(Faktura.DataWystawienia):
                        List = new ObservableCollection<FakturaForAllView>(OrderDescending
                            ? List.OrderByDescending(item => item.DataWystawienia)
                            : List.OrderBy(item => item.DataWystawienia));
                        break;
                };
            }
        }

        protected override void Open()
        {
            if (CzyModyfikowac)
            {
                //Otworzyc widok modyfikacji wybranego elemntu
            }
            else
            {
                if (SelectedItem != null)
                {
                    Messenger.Default.Send(SelectedItem);
                    OnRequestClose();
                }
            }

        }

        protected override void ShowAddView()
        {
            Messenger.Default.Send(DisplayName + " Add");
        }

        protected override void Refresh()
        {
            Load();
        }

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                try
                {
                    erpEntities.Faktura.First(item => item.IdFaktury == SelectedItem.IdFaktury).CzyAktywna = false;
                    erpEntities.SaveChanges();
                    Refresh();
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Wystąpił błąd usuwania\n{e.Message}");
                }
            }
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsFilter()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(Faktura.Numer), "Numer"),
                new KeyValuePair<string, string>(nameof(Faktura.Kontrahent.Nazwa), "Nazwa kontrahenta"),
            };
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsOrderBy()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(Faktura.DataWystawienia), "Data wystawienia"),
                new KeyValuePair<string, string>(nameof(Faktura.Numer), "Numer"),
            };
        }
        #endregion
    }
}
