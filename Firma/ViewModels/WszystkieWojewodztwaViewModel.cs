using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels
{
    public class WszystkieWojewodztwaViewModel : WszystkieViewModel<Wojewodztwo>
    {
        #region  Constructor
        public  WszystkieWojewodztwaViewModel() : base(ViewResources.BaseResources.Wojewodztwa)
        {
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<Wojewodztwo>
            (
                from wojewodztwo in erpEntities.Wojewodztwo 
                where wojewodztwo.IsActive == true
                select wojewodztwo
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
