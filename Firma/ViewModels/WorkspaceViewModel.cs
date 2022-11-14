using System;
using System.Windows.Input;
using Firma.Helpers;

namespace Firma.ViewModels
{
    public class WorkspaceViewModel : BaseViewModel
    {
        #region Pola i komendy
        public string DisplayName { get; set; }

        private BaseCommand _CloseCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new BaseCommand(() => onRequestClose());
                }

                return _CloseCommand;
            }
        }
        #endregion

        #region RequestClose 
        public event EventHandler RequestClose;

        private void onRequestClose()
        {
            EventHandler handler = RequestClose;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
