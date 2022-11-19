using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.Entities;
using Firma.ViewResources;

namespace Firma.ViewModels
{
    public class WszystkieTowaryViewModel : WorkspaceViewModel
    {
        #region  Fields
        //polaczenie  z  baza  danych
        private  readonly  ERPEntities  erpEntities; 
        private  BaseCommand  _LoadCommand;
        //lista  towarow  zaladowana  z  bazy  danych
        private  ObservableCollection<Towary>  _TowaryList; 
        #endregion  
        
        #region  Properties
        public  ICommand  LoadCommand
        {
            get
            {
                if  (_LoadCommand  ==  null)
                {
                    _LoadCommand  =  new  BaseCommand(()  =>  load());
                }
                return  _LoadCommand;
            }
        }
        public  ObservableCollection<Towary>  TowaryList
        {
 
            get
            {

                if  (_TowaryList  ==  null) load();
                return  _TowaryList;


            }
            set
            {
                _TowaryList  =  value; OnPropertyChanged(()  =>  TowaryList);
            }
        }
        #endregion  //Properties
        
        #region  Constructor
        public  WszystkieTowaryViewModel()
        {
            base.DisplayName  =  "Wszystkie  towary";
            erpEntities  =  new  ERPEntities();
        }
        #endregion  //  Constructor
        
        #region  Helpers
        private  void  load()
        {
            TowaryList  =  new  ObservableCollection<Towary>
                (from  towar  in  erpEntities.Towary  select  towar);
        }
        #endregion

    }
}
