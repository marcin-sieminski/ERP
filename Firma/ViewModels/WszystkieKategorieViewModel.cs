using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;

namespace Firma.ViewModels
{
    public class WszystkieKategorieViewModel : WszystkieViewModel<Kategoria>
    {
        #region  Constructor
        public  WszystkieKategorieViewModel() : base(ViewResources.BaseResources.Kategorie)
        {
        }
        #endregion
        
        #region  Helpers
        public override void Load()
        {
            List = new ObservableCollection<Kategoria>
            (
                from kategoria in ERPEntities.Kategoria 
                where kategoria.IsActive == true
                select kategoria
            );
        }
        #endregion
    }
}
