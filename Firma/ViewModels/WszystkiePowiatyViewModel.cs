using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels
{
    public class WszystkiePowiatyViewModel : WszystkieViewModel<Powiat>
    {
        #region  Constructor
        public  WszystkiePowiatyViewModel() : base(ViewResources.BaseResources.Powiaty)
        {
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<Powiat>
            (
                from powiat in erpEntities.Powiat 
                where powiat.IsActive == true
                select powiat
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
