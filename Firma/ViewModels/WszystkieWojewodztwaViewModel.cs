using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;

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
                from wojewodztwo in ERPEntities.Wojewodztwo 
                where wojewodztwo.IsActive == true
                select wojewodztwo
            );
        }
        #endregion
    }
}
