using System.Collections.Generic;
using System.Collections.ObjectModel;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using Firma.ViewResources;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using System;
using Firma.Models.Entities;

namespace Firma.ViewModels
{
    public class WszyscyPracownicyViewModel : WszystkieViewModel<PracownikForAllView>
    {
        public bool CzyModyfikowac { get; set; }

        #region Konstruktory

        public WszyscyPracownicyViewModel() : base(BaseResources.Pracownicy)
        {
            CzyModyfikowac = true;
        }

        #endregion

        public override void Load()
        {
            AllList = erpEntities.Pracownik.Where(x => x.IsActive == true)
                .Select(x => new PracownikForAllView()
                {
                    Id = x.Id,
                    Nazwisko = x.Nazwisko,
                    Imie = x.Imie,
                    DataUrodzenia = x.DataUr,
                    ZatrudnionyOd = x.ZatrudnionyOd,
                    ZatrudnionyDo = x.ZatrudnionyDo,
                    Kraj = x.Kraj.Nazwa,
                    Miasto = x.Miasto.Nazwa,
                    Wojewodztwo = x.Wojewodztwo.Nazwa,
                    Gmina = x.Gmina.Nazwa,
                    Wyksztalcenie = x.Wyksztalcenie.Nazwa,
                    StanCywilny = x.StanCywilny,
                    NrRachunku = x.RachunekNr,
                    DowodOsobisty = x.DowOsobisty
                })
                .ToList();

            List = new ObservableCollection<PracownikForAllView>(AllList);

            Filter();
            OrderBy();
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

        protected override void Filter()
        {
            if (string.IsNullOrEmpty(SearchPhrase))
                return;

            switch (SelectedFilter)
            {
                case nameof(Pracownik.Nazwisko):
                    List = new ObservableCollection<PracownikForAllView>(AllList
                        .Where(item => item.Nazwisko?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                    break;
                case nameof(Pracownik.Miasto):
                    List = new ObservableCollection<PracownikForAllView>(AllList
                        .Where(item => item.Miasto?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                    break;
                case nameof(Pracownik.Wojewodztwo):
                    List = new ObservableCollection<PracownikForAllView>(AllList
                        .Where(item => item.Wojewodztwo?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                    break;
                default:
                    List = new ObservableCollection<PracownikForAllView>(AllList);
                    break;
            };
        }

        protected override void OrderBy()
        {
            if (string.IsNullOrEmpty(SelectedOrderBy))
                return;

            switch (SelectedOrderBy)
            {
                case nameof(Pracownik.Nazwisko):
                    List = new ObservableCollection<PracownikForAllView>(OrderDescending
                        ? List.OrderByDescending(item => item.Nazwisko)
                        : List.OrderBy(item => item.Nazwisko));
                    break;
                case nameof(Pracownik.DataUr):
                    List = new ObservableCollection<PracownikForAllView>(OrderDescending
                        ? List.OrderByDescending(item => item.DataUrodzenia)
                        : List.OrderBy(item => item.DataUrodzenia));
                    break;
                case nameof(Pracownik.ZatrudnionyOd):
                    List = new ObservableCollection<PracownikForAllView>(OrderDescending
                        ? List.OrderByDescending(item => item.ZatrudnionyOd)
                        : List.OrderBy(item => item.ZatrudnionyOd));
                    break;
            };
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsFilter()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(Pracownik.Nazwisko), "Nazwisko"),
                new KeyValuePair<string, string>(nameof(Pracownik.Miasto), "Miasto"),
                new KeyValuePair<string, string>(nameof(Pracownik.Wojewodztwo), "Województwo"),
            };
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsOrderBy()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(Pracownik.Nazwisko), "Nazwisko"),
                new KeyValuePair<string, string>(nameof(Pracownik.DataUr), "Data urodzenia"),
                new KeyValuePair<string, string>(nameof(Pracownik.ZatrudnionyOd), "Data zatrudnienia"),
            };
        }
    }
}