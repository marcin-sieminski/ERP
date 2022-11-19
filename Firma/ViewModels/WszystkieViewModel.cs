using Firma.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Firma.Models.Entities;

namespace Firma.ViewModels
{
    public abstract class WszystkieViewModel<T> : WorkspaceViewModel
    {
        #region Fields
        private readonly ERPEntities erpEntities;
        public ERPEntities ERPEntities => erpEntities;

        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand==null)
                {
                    _LoadCommand = new BaseCommand(() => Load());
                    return _LoadCommand;
                }

                return _LoadCommand;
            }
        }

        private ObservableCollection<T> _List;
        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null)
                {
                    Load();
                }

                return _List;
            }

            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }
        #endregion

        #region Konstruktor
        public WszystkieViewModel(string displayName)
        {
            base.DisplayName = displayName;
            erpEntities = new ERPEntities();
        }
        #endregion

        #region Helpers
        public abstract void Load();
        #endregion
    }
}
