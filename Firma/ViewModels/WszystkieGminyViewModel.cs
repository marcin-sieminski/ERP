using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.Entities;

namespace Firma.ViewModels
{
    public class WszystkieGminyViewModel : WszystkieViewModel<Gminy>
    {
        #region  Constructor
        public  WszystkieGminyViewModel() : base("Gminy")
        {
            base.DisplayName  =  "Gminy";
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<Gminy>
            (
                from gmina in ERPEntities.Gminy 
                select gmina
            );
        }
        #endregion
    }
}
