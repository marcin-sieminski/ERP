using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using Firma.ViewResources;

namespace Firma.ViewModels
{
    public class WszystkieGminyViewModel : WszystkieViewModel<Gmina>
    {
        #region  Constructor
        public  WszystkieGminyViewModel() : base(BaseResources.Gminy)
        {
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<Gmina>
            (
                from gmina in erpEntities.Gmina 
                where gmina.IsActive == true
                select gmina
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
