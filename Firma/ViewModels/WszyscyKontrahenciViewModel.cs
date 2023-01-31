using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using Firma.Models.Entities;
using System.Diagnostics;

namespace Firma.ViewModels
{
    public class WszyscyKontrahenciViewModel : WszystkieViewModel<KontrahentForAllView>
    {
        public bool CzyModyfikowac { get; set; }

        public WszyscyKontrahenciViewModel() : base("Kontrahenci")
        {
            CzyModyfikowac = false;
        }

        public override void Load()
        {
            AllList = erpEntities.Kontrahent.Where(x => x.IsActive)
                .Include("Adres")
                .Select(x => new KontrahentForAllView()
                {
                    Id = x.Id,
                    Kod = x.Kod,
                    Nazwa = x.Nazwa,
                    NIP = x.Nip,
                    Ulica = x.Adres.Ulica,
                    Miejscowosc = x.Adres.Miejscowosc,
                    KodPocztowy = x.Adres.KodPocztowy,
                    Kraj = x.Adres.Kraj,
                    Telefon = x.Telefon,
                    EMail = x.Email,
                    NrRachunku = x.RachunekNr,
                    OpiekunKontrahenta = x.Pracownik.Nazwisko
                }).ToList();

            List = new ObservableCollection<KontrahentForAllView>(AllList);

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
                }
            }
        }

        protected override void ShowAddView()
        {
            return;
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
                    erpEntities.Kontrahent.First(item => item.Id == SelectedItem.Id).IsActive = false;
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
                case nameof(Kontrahent.Nazwa):
                    List = new ObservableCollection<KontrahentForAllView>(AllList
                        .Where(item => item.Nazwa?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                    break;
                case nameof(Kontrahent.Kod):
                    List = new ObservableCollection<KontrahentForAllView>(AllList
                        .Where(item => item.Kod?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                    break;
                default:
                    List = new ObservableCollection<KontrahentForAllView>(AllList);
                    break;
            };
        }

        protected override void OrderBy()
        {
            if (string.IsNullOrEmpty(SelectedOrderBy))
                return;

            switch (SelectedOrderBy)
            {
                case nameof(Kontrahent.Nazwa):
                    List = new ObservableCollection<KontrahentForAllView>(OrderDescending
                        ? List.OrderByDescending(item => item.Nazwa)
                        : List.OrderBy(item => item.Nazwa));
                    break;
                case nameof(Kontrahent.Miasto):
                    List = new ObservableCollection<KontrahentForAllView>(OrderDescending
                        ? List.OrderByDescending(item => item.Miejscowosc)
                        : List.OrderBy(item => item.Miejscowosc));
                    break;
            };
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsFilter()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(Kontrahent.Nazwa), "Nazwa"),
                new KeyValuePair<string, string>(nameof(Kontrahent.Kod), "Kod"),
            };
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsOrderBy()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(Kontrahent.Nazwa), "Nazwa"),
                new KeyValuePair<string, string>(nameof(Kontrahent.Adres.Miejscowosc), "Miejscowość"),
            };
        }
    }
}