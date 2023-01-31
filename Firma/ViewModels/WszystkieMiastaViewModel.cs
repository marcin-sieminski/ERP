using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;

namespace Firma.ViewModels
{
    public class WszystkieMiastaViewModel : WszystkieViewModel<Miasto>
    {
        public bool CzyModyfikowac { get; set; }

        #region  Constructor
        public WszystkieMiastaViewModel() : base("Miasta")
        {
            CzyModyfikowac = false;
        }
        #endregion

        #region  Helpers
        public override void Load()
        {
            AllList = new ObservableCollection<Miasto>
            (
                from miasto in erpEntities.Miasto
                where miasto.IsActive == true
                select miasto
            ).ToList();
            Filter();
        }

        protected override void Filter()
        {
            if (!string.IsNullOrEmpty(SearchPhrase))
            {
                switch (SelectedFilter)
                {
                    case nameof(Gmina.Nazwa):
                        List = new ObservableCollection<Miasto>(AllList
                            .Where(item => item.Nazwa?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                        break;
                    default:
                        List = new ObservableCollection<Miasto>(AllList);
                        break;
                };
            }
            else
            {
                List = new ObservableCollection<Miasto>(AllList);
                OrderBy();
            }
        }

        protected override void OrderBy()
        {
            if (!string.IsNullOrEmpty(SelectedOrderBy))
            {
                switch (SelectedOrderBy)
                {
                    case nameof(Miasto.Nazwa):
                        List = new ObservableCollection<Miasto>(OrderDescending
                            ? List.OrderByDescending(item => item.Nazwa)
                            : List.OrderBy(item => item.Nazwa));
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
                    erpEntities.Miasto.First(item => item.Id == SelectedItem.Id).IsActive = false;
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
                new KeyValuePair<string, string>(nameof(Miasto.Nazwa), "Nazwa"),
            };
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsOrderBy()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(Miasto.Nazwa), "Nazwa"),
            };
        }

        #endregion
    }
}
