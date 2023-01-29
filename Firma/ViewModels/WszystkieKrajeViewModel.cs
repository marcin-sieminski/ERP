using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels
{
    public class WszystkieKrajeViewModel : WszystkieViewModel<Kraj>
    {
        #region  Constructor
        public  WszystkieKrajeViewModel() : base("Kraje")
        {
            base.DisplayName  =  "Kraje";
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<Kraj>
            (
                from kraj in erpEntities.Kraj 
                where kraj.IsActive == true
                select kraj
            );
        }

        protected override void Open()
        {
            throw new System.NotImplementedException();
        }

        protected override void Filter()
        {
            throw new System.NotImplementedException();
        }

        protected override void OrderBy()
        {
            throw new System.NotImplementedException();
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsFilter()
        {
            throw new System.NotImplementedException();
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsOrderBy()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
