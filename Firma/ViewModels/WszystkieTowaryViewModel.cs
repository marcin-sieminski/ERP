using Firma.Models.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using Firma.ViewModels.Abstract;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;

namespace Firma.ViewModels
{
    public class WszystkieTowaryViewModel : WszystkieViewModel<Towar> //bo wszystkie zakładki dziedzicza po WorkspaceViewModel
    {
        #region Konstruktor
        public WszystkieTowaryViewModel() : base("Towary")
        {

        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Towar>
            (
                from towar in erpEntities.Towar
                where towar.IsActive == true
                select towar
            );
        }

        protected override void Filter()
        {
            throw new NotImplementedException();
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsFilter()
        {
            throw new NotImplementedException();
        }

        protected override List<KeyValuePair<string, string>> GetListOfItemsOrderBy()
        {
            throw new NotImplementedException();
        }

        protected override void Open()
        {
            throw new NotImplementedException();
        }

        protected override void OrderBy()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}