using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels
{
    public class WszystkieMiastaViewModel : WszystkieViewModel<Miasto>
    {
        #region  Constructor
        public  WszystkieMiastaViewModel() : base("Miasta")
        {
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<Miasto>
            (
                from miasto in erpEntities.Miasto 
                where miasto.IsActive == true
                select miasto
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
