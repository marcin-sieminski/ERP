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
    public class WszyscyProducenciViewModel : WszystkieViewModel<Producent>
    {
        public bool CzyModyfikowac { get; set; }

        #region  Constructor
        public  WszyscyProducenciViewModel() : base(ViewResources.BaseResources.Producenci)
        {
            CzyModyfikowac = false;
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            AllList = new ObservableCollection<Producent>
            (
                from producent in erpEntities.Producent 
                where producent.IsActive == true
                select producent
            ).ToList();
            Filter();
        }


                protected override void Filter()
        {
            if (!string.IsNullOrEmpty(SearchPhrase))
            {
                switch (SelectedFilter)
                {
                    case nameof(Producent.Nazwa):
                        List = new ObservableCollection<Producent>(AllList
                            .Where(item => item.Nazwa?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                        break;
                    case nameof(Producent.Kod):
                        List = new ObservableCollection<Producent>(AllList
                            .Where(item => item.Kod?.ToLower().Contains(SearchPhrase.ToLower()) ?? false));
                        break;
                    default:
                        List = new ObservableCollection<Producent>(AllList);
                        break;
                };
            }
            else
            {
                List = new ObservableCollection<Producent>(AllList);
                OrderBy();
            }
        }

        protected override void OrderBy()
        {
            if (!string.IsNullOrEmpty(SelectedOrderBy))
            {
                switch (SelectedOrderBy)
                {
                    case nameof(Producent.Nazwa):
                        List = new ObservableCollection<Producent>(OrderDescending
                            ? List.OrderByDescending(item => item.Nazwa)
                            : List.OrderBy(item => item.Nazwa));
                        break;
                    case nameof(Producent.Kod):
                        List = new ObservableCollection<Producent>(OrderDescending
                            ? List.OrderByDescending(item => item.Kod)
                            : List.OrderBy(item => item.Kod));
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
                erpEntities.Producent.First(item => item.Id == SelectedItem.Id).IsActive = false;
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
                new KeyValuePair<string, string>(nameof(Producent.Nazwa), "Nazwa"),
                new KeyValuePair<string, string>(nameof(Producent.Kod), "Kod"),
            };
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsOrderBy()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(Producent.Nazwa), "Nazwa"),
                new KeyValuePair<string, string>(nameof(Producent.Kod), "Kod"),
            };
        }
        #endregion
    }
}
