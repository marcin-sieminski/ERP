using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;

namespace Firma.ViewModels
{
    public class WszystkieWyksztalceniaViewModel : WszystkieViewModel<Wyksztalcenie>
    {
        #region  Constructor
        public  WszystkieWyksztalceniaViewModel() : base(ViewResources.BaseResources.Wyksztalcenia)
        {
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<Wyksztalcenie>
            (
                from wyksztalcenie in ERPEntities.Wyksztalcenie 
                where wyksztalcenie.IsActive == true
                select wyksztalcenie
            );
        }
        #endregion
    }
}
