using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels
{
    public class WszystkieKartyKredytoweViewModel : WszystkieViewModel<KartaKredytowa>
    {
        #region  Constructor
        public  WszystkieKartyKredytoweViewModel() : base(ViewResources.BaseResources.KartyKredytowe)
        {
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<KartaKredytowa>
            (
                from kartaKredytowa in erpEntities.KartaKredytowa 
                where kartaKredytowa.IsActive == true
                select kartaKredytowa
            );
        }

        protected override void Open()
        {
            throw new System.NotImplementedException();
        }

        protected override void ShowAddView()
        {
            throw new System.NotImplementedException();
        }

        protected override void Refresh()
        {
            throw new System.NotImplementedException();
        }

        protected override void Delete()
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
