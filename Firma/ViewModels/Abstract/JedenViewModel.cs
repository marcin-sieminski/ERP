using Firma.Helpers;
using System.Windows;
using System.Windows.Input;
using Firma.Models.Entities;

namespace Firma.ViewModels.Abstract
{
    public abstract class JedenViewModel<T> : WorkspaceViewModel 
    {
        #region Fields
        public ERPEntities Db { get; set; }
        public T Item { get; set; }
        #endregion

        #region Konstruktor
        public JedenViewModel(string displayName)
        {
            base.DisplayName = displayName;
            Db = new ERPEntities();
        }
        #endregion

        #region Command
        private BaseCommand _SaveAndCloseCommand;
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null)
                    _SaveAndCloseCommand = new BaseCommand(() => SaveAndClose());
                return _SaveAndCloseCommand;
            }
        }
        #endregion

        #region Methods

        protected virtual bool IsValid() => true;
        protected abstract void Save();
        protected void SaveAndClose()
        {
            if(IsValid())
            {
                Save();
                base.OnRequestClose();
                MessageBox.Show("Zapisano pomyślnie", "Sukces");
            }
        }
        #endregion
    }
}
