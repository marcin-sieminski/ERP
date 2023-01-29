using Firma.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Firma.Models.Entities;

namespace Firma.ViewModels.Abstract
{
    public abstract class WszystkieViewModel<T> : WorkspaceViewModel 
        where T : class
    {
        #region Pola, komendy
        protected readonly ERPEntities erpEntities;
        private BaseCommand _LoadCommand;
        private ObservableCollection<T> _List;


        private T _SelectedItem;
        public T SelectedItem 
        {
            get => _SelectedItem;
            set
            {
                if(_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged(() => SelectedItem);
                }
            }
        }

        public ERPEntities ErpEntities { get => erpEntities; }
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                {
                    _LoadCommand = new BaseCommand(() => Load());
                }
                return _LoadCommand;
            }
        }
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
        public List<T> AllList { get; set; }

        private ICommand _OpenCommand;
        public ICommand OpenCommand
        {
            get
            {
                if(_OpenCommand == null)
                {
                    _OpenCommand = new BaseCommand(() => Open());
                }
                return _OpenCommand;
            }
        }

        public List<KeyValuePair<string, string>> ListOfItemsOrderBy { get; set; }
        private bool _OrderDescending;
        public bool OrderDescending 
        {
            get => _OrderDescending;
            set
            {
                if(_OrderDescending != value)
                {
                    _OrderDescending = value;
                    OnPropertyChanged(() => OrderDescending);
                }
            } 
        }
        public List<KeyValuePair<string, string>> ListOfItemsFilter { get; set; }
        private string _SearchPhrase;
        public string SearchPhrase
        {
            get => _SearchPhrase;
            set
            {
                if (_SearchPhrase != value)
                {
                    _SearchPhrase = value;
                    OnPropertyChanged(() => SearchPhrase);
                }
            }
        }

        private ICommand _OrderByCommand;
        public ICommand OrderByCommand
        {
            get
            {
                if (_OrderByCommand == null)
                {
                    _OrderByCommand = new BaseCommand(() => OrderBy());
                }
                return _OrderByCommand;
            }
        }

        private ICommand _FilterCommand;
        public ICommand FilterCommand
        {
            get
            {
                if (_FilterCommand == null)
                {
                    _FilterCommand = new BaseCommand(() => Filter());
                }
                return _FilterCommand;
            }
        }

        private string _SelectedOrderBy;
        public string SelectedOrderBy
        {
            get => _SelectedOrderBy;
            set
            {
                if (_SelectedOrderBy != value)
                {
                    _SelectedOrderBy = value;
                    OnPropertyChanged(() => SelectedOrderBy);
                }
            }
        }

        private string _SelectedFilter;
        public string SelectedFilter
        {
            get => _SelectedFilter;
            set
            {
                if (_SelectedFilter != value)
                {
                    _SelectedFilter = value;
                    OnPropertyChanged(() => SelectedFilter);
                }
            }
        }
        #endregion

        #region Konstruktor
        public WszystkieViewModel(string displayName)
        {
            base.DisplayName = displayName;
            this.erpEntities = new ERPEntities();
            ListOfItemsFilter = GetListOfItemsFilter();
            ListOfItemsOrderBy = GetListOfItemsOrderBy();
        }
        #endregion

        #region Helpers
        public abstract void Load();
        protected abstract void Open();
        protected abstract void Filter();
        protected abstract void OrderBy();
        protected abstract List<KeyValuePair<string, string>> GetListOfItemsFilter();
        protected abstract List<KeyValuePair<string, string>> GetListOfItemsOrderBy();
        #endregion
    }
}
