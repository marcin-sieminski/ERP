using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;

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
                from kartaKredytowa in ERPEntities.KartaKredytowa 
                where kartaKredytowa.IsActive == true
                select kartaKredytowa
            );
        }
        #endregion
    }
}
