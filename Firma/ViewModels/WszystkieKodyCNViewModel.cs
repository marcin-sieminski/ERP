using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels
{
    public class WszystkieKodyCNViewModel : WszystkieViewModel<KodCN>
    {
        #region  Constructor
        public  WszystkieKodyCNViewModel() : base(ViewResources.BaseResources.KodyCN)
        {
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<KodCN>
            (
                from kod in erpEntities.KodCN 
                where kod.IsActive == true
                select kod
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
