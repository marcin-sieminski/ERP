using Firma.Models.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using Firma.Models.EntitiesForView;
using System.Diagnostics;
using GalaSoft.MvvmLight.Messaging;

namespace Firma.ViewModels
{
    public class WszystkieTowaryViewModel : WszystkieViewModel<TowarForAllView>
    {
        public bool CzyModyfikowac { get; set; }

        #region Konstruktor
        public WszystkieTowaryViewModel() : base("Towary")
        {
            CzyModyfikowac = true;
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            AllList = erpEntities.Towar.Where(x => x.IsActive == true).Select(x => new TowarForAllView()
            {
                Id = x.Id,
                KodCN = x.KodCN.Kod,
                Kod = x.Kod,
                NumerKatalogowy = x.NumerKat,
                Nazwa = x.Nazwa,
                URL = x.URL,
                KategoriaKod = x.Kategoria.Kod,
                Opis = x.Opis,
                JM = x.JM,
                Waluta = x.Waluta,
                Cena = x.Cena,
                NazwaKontrahenta = x.Kontrahent.Nazwa,
                NazwaProducenta = x.Producent.Nazwa,
                Waga = x.WagaKG.Value,
                OpiekunImieNazwisko = x.Pracownik.Imie + " " + x.Pracownik.Nazwisko
            }).ToList();

            Filter();
        }

        protected override void Filter()
        {
            if (!string.IsNullOrEmpty(SearchPhrase))
            {
                switch (SelectedFilter)
                {
                    case nameof(Towar.Nazwa):
                        List = new ObservableCollection<TowarForAllView>(AllList
                            .Where(item => item.Nazwa?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                        break;
                    case nameof(Towar.Opis):
                        List = new ObservableCollection<TowarForAllView>(AllList
                            .Where(item => item.Opis?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                        break;
                    default:
                        List = new ObservableCollection<TowarForAllView>(AllList);
                        break;
                };
            }
            else
            {
                List = new ObservableCollection<TowarForAllView>(AllList);
                OrderBy();
            }
        }

        protected override void OrderBy()
        {
            if (!string.IsNullOrEmpty(SelectedOrderBy))
            {
                switch (SelectedOrderBy)
                {
                    case nameof(Towar.Nazwa):
                        List = new ObservableCollection<TowarForAllView>(OrderDescending
                            ? List.OrderByDescending(item => item.Nazwa)
                            : List.OrderBy(item => item.Nazwa));
                        break;
                    case nameof(Towar.Cena):
                        List = new ObservableCollection<TowarForAllView>(OrderDescending
                            ? List.OrderByDescending(item => item.Cena)
                            : List.OrderBy(item => item.Cena));
                        break;
                };
            }
        }

        protected override void Open()
        {
            if (SelectedItem != null)
            {

                if (CzyModyfikowac)
                {
                    Messenger.Default.Send(DisplayName + " Edit." + SelectedItem.Id);
                }
                else
                {
                    Messenger.Default.Send(SelectedItem);
                    OnRequestClose();
                    CzyModyfikowac = true;
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
                erpEntities.Towar.First(item => item.Id == SelectedItem.Id).IsActive = false;
                try
                {
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
                new KeyValuePair<string, string>(nameof(Towar.Nazwa), "Nazwa"),
                new KeyValuePair<string, string>(nameof(Towar.Opis), "Opis"),
            };
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsOrderBy()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(Towar.Nazwa), "Nazwa"),
                new KeyValuePair<string, string>(nameof(Towar.Cena), "Cena"),
            };
        }
        #endregion

    }
}