using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;

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
                from miasto in ERPEntities.Miasto 
                where miasto.IsActive == true
                select miasto
            );
        }
        #endregion
    }
}
