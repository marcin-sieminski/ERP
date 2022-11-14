using System;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class CommandViewModel : BaseViewModel
    {
        #region Właściwości
        public string DisplayName { get; set; }
        public ICommand Command { get; set; }
        #endregion

        #region Konstruktor
        public CommandViewModel(string displayName, ICommand command)
        {
            Command = command ?? throw new ArgumentException("Command");
            DisplayName = displayName;
        }
        #endregion
    }
}