using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.Entities;
using Firma.ViewResources;

namespace Firma.ViewModels
{
    public class NowyTowarViewModel : WorkspaceViewModel
    {
        #region Fields
        private ERPEntities entities; 
        private Towar towar;
        private BaseCommand _SaveCommand;
        #endregion
        
        #region Konstruktor

        public NowyTowarViewModel()
        {
            DisplayName = BaseResources.NowyTowar;
            entities = new ERPEntities();
            towar = new Towar();
        }
        #endregion

        #region Properties 
        public string Nazwa
        {
            get
            {
                return towar.Nazwa;
            }
            set
            {
                if (value  ==  towar.Nazwa) return;
                towar.Nazwa  =  value; OnPropertyChanged(()=>Nazwa);
            }
 }
        #endregion  //Properties

        #region  Command
        public  ICommand  SaveCommand
        {
            get
            {
                if  (_SaveCommand  ==  null)
                {
                    _SaveCommand  =  new  BaseCommand(()  =>  saveAndClose());
                }
                return  _SaveCommand;
            }
        }
        #endregion  //Command
        
        #region  Helpers 
        public  void  Save()
        {
            entities.Towar.AddObject(towar);
            entities.SaveChanges();
        }
        private  void  saveAndClose()
        {
            Save();
        }
        #endregion

    }
}
