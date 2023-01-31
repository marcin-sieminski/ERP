using Firma.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Firma.ViewModels.Abstract
{
    public abstract class JedenWszystkieViewModel<JedenType, WszystkieType> : JedenViewModel<JedenType>
        where JedenType : class
    {
        #region Pola i właściwości

        private ObservableCollection<WszystkieType> _WszystkieList;
        public ObservableCollection<WszystkieType> WszystkieList 
        {
            get => _WszystkieList;
            set
            {
                if(value != _WszystkieList)
                {
                    _WszystkieList = value;
                    OnPropertyChanged(() => WszystkieList);
                }
            }
        }

        private ICommand _ShowAddViewCommand;
        public ICommand ShowAddViewCommand
        {
            get
            {
                if (_ShowAddViewCommand == null)
                    _ShowAddViewCommand = new BaseCommand(() => ShowAddView());
                return _ShowAddViewCommand;
            }
        }

        public string WszystkieDisplayName { get; set; }
        public string ShowAddViewButtonContent { get; set; }

        #endregion

        public JedenWszystkieViewModel(string displayName, string wszystkieDisplayName, string showAddViewButtonContent)
            : base(displayName)
        {
            WszystkieDisplayName = wszystkieDisplayName;
            ShowAddViewButtonContent = showAddViewButtonContent;
        }


        #region Metody
        protected abstract void ShowAddView();
        #endregion
    }
}
