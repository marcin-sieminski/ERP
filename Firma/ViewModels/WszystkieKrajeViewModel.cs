using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.Entities;

namespace Firma.ViewModels
{
    public class WszystkieKrajeViewModel : WszystkieViewModel<Kraj>
    {
        #region  Constructor
        public  WszystkieKrajeViewModel() : base("Kraje")
        {
            base.DisplayName  =  "Kraje";
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<Kraj>
            (
                from kraj in ERPEntities.Kraj 
                where kraj.IsActive == true
                select kraj
            );
        }
        #endregion
    }
}
