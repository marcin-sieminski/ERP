using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;

namespace Firma.ViewModels
{
    public class WszyscyProducenciViewModel : WszystkieViewModel<Producent>
    {
        #region  Constructor
        public  WszyscyProducenciViewModel() : base(ViewResources.BaseResources.Producenci)
        {
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<Producent>
            (
                from producent in ERPEntities.Producent 
                where producent.IsActive == true
                select producent
            );
        }
        #endregion
    }
}
