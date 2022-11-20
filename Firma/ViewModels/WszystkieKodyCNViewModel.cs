using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;

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
                from kod in ERPEntities.KodCN 
                where kod.IsActive == true
                select kod
            );
        }
        #endregion
    }
}
