using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;

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
                from powiat in ERPEntities.Powiat 
                where powiat.IsActive == true
                select powiat
            );
        }
        #endregion
    }
}
