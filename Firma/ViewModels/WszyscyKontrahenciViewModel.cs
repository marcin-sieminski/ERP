using Firma.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels
{
    public class WszyscyKontrahenciViewModel : WszystkieViewModel<Kontrahent>
    {
        public bool CzyModyfikowac { get; set; }

        public WszyscyKontrahenciViewModel() : base("Kontrahenci")
        {
            CzyModyfikowac = false;
        }

        public override void Load()
        {
            List = new ObservableCollection<Kontrahent>
            (
                erpEntities.Kontrahent.Where(item => item.IsActive == true).ToList()
            );
        }

        protected override void Open()
        {
            if(CzyModyfikowac)
            {
                //Otworzyc widok modyfikacji wybranego elemntu
            }
            else
            {
                if(SelectedItem != null)
                {
                    Messenger.Default.Send(SelectedItem);
                    OnRequestClose();
                }
            }
        }

        protected override void Filter()
        {
            throw new NotImplementedException();
        }

        protected override void OrderBy()
        {
            throw new NotImplementedException();
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsFilter()
        {
            return new List<KeyValuePair<string, string>>();
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsOrderBy()
        {
            return new List<KeyValuePair<string, string>>();
        }
    }
}