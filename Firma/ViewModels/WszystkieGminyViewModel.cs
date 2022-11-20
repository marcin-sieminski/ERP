using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.Entities;
using Firma.ViewResources;

namespace Firma.ViewModels
{
    public class WszystkieGminyViewModel : WszystkieViewModel<Gmina>
    {
        #region  Constructor
        public  WszystkieGminyViewModel() : base(BaseResources.Gminy)
        {
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<Gmina>
            (
                from gmina in ERPEntities.Gmina 
                where gmina.IsActive == true
                select gmina
            );
        }
        #endregion
    }
}
