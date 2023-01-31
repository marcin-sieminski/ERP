using Firma.Helpers;
using Firma.Models.EntitiesForView;
using Firma.ViewResources;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            _WidocznoscMenuBocznego = "Visible";
            Messenger.Default.Register<string>(this, message => Open(message));
        }

        #region Przyciski w menu z lewej strony
        private ReadOnlyCollection<CommandViewModel> _Commands { get; set; }

        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel> cmds = CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }

        private List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
        {
            new CommandViewModel(BaseResources.Faktury, new BaseCommand(showAllFaktury)),
            //new CommandViewModel(BaseResources.NowaFaktura, new BaseCommand(() => createView(new NowaFakturaViewModel()))),
            new CommandViewModel(BaseResources.Towary, new BaseCommand(showAllTowar)),
            //new CommandViewModel(BaseResources.NowyTowar, new BaseCommand(() => createView(new NowyTowarViewModel()))),
            new CommandViewModel(BaseResources.Kontrahenci, new BaseCommand(showAllKontrahenci)),
            //new CommandViewModel(BaseResources.NowyKontrahent, new BaseCommand(() => createView(new NowyKontrahentViewModel()))),
            new CommandViewModel(BaseResources.Pracownicy, new BaseCommand(showAllPracownicy)),
            //new CommandViewModel(BaseResources.NowyPracownik, new BaseCommand(() => createView(new NowyPracownikViewModel()))),
            new CommandViewModel(BaseResources.Producenci, new BaseCommand(showAllProducenci)),
            new CommandViewModel(BaseResources.Gminy, new BaseCommand(showAllGmina)),
            new CommandViewModel(BaseResources.Kraje, new BaseCommand(showAllKraj)),
            new CommandViewModel(BaseResources.Miasta, new BaseCommand(showAllMiasto)),
            new CommandViewModel(BaseResources.Powiaty, new BaseCommand(showAllPowiat)),
            new CommandViewModel(BaseResources.Wojewodztwa, new BaseCommand(showAllWojewodztwo)),
            new CommandViewModel(BaseResources.Wyksztalcenia, new BaseCommand(showAllWyksztalcenie)),
            new CommandViewModel(BaseResources.KodyCN, new BaseCommand(showAllKodCN)),
            new CommandViewModel(BaseResources.KartyKredytowe, new BaseCommand(showAllKartyKredytowe)),
            new CommandViewModel(BaseResources.Kategorie, new BaseCommand(showAllKategorie)),
            new CommandViewModel(BaseResources.Raport, new BaseCommand(showRaport))
        };
        }
        #endregion

        #region Zakładki
        private ObservableCollection<WorkspaceViewModel> _Workspaces { get; set; }

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += onWorkspacesChanged;
                }

                return _Workspaces;
            }
        }

        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
            {
                foreach (WorkspaceViewModel workspace in e.NewItems)
                {
                    workspace.RequestClose += this.onWorkspaceRequestClose;
                }
            }

            if (e.OldItems != null && e.OldItems.Count != 0)
            {
                foreach (WorkspaceViewModel workspace in e.OldItems)
                {
                    workspace.RequestClose -= this.onWorkspaceRequestClose;
                }
            }
        }

        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            var workspace = sender as WorkspaceViewModel;
            this.Workspaces.Remove(workspace);
        }
        
        private void Open(string message)
        {
            switch (message)
            {
                case "Towary Show":
                    WszystkieTowaryViewModel wszystkieTowaryViewModel = showAllTowaryMessanger();
                    wszystkieTowaryViewModel.CzyModyfikowac = false;
                    break;
                case "Kontrahenci Show":
                    WszyscyKontrahenciViewModel wszyscyKontrahenciViewModel = showAllKontrahenciMessanger();
                    wszyscyKontrahenciViewModel.CzyModyfikowac = false;
                    break;
                case "Pracownicy Show":
                    WszyscyPracownicyViewModel viewModel = showAllPracownicyMessanger();
                    viewModel.CzyModyfikowac = false;
                    break;
                case "Pozycje faktury Add":
                    createView(new NowaPozycjaFakturyViewModel());
                    break;
                case "Faktury Add":
                    createView(new NowaFakturaViewModel());
                    break;
                case "Towary Add":
                    createView(new NowyTowarViewModel());
                    break;
                case "Gminy Add":
                    createView(new NowaGminaViewModel());
                    break;
                case "Kraje Add":
                    createView(new NowyKrajViewModel());
                    break;
                case "Pracownicy Add":
                    createView(new NowyPracownikViewModel());
                    break;
            };
        }

        #endregion

        #region Funkcje pomocnicze

        private void createView(WorkspaceViewModel workspace)
        {
            Workspaces.Add(workspace);
            setActiveWorkspace(workspace);
        }

        private void ShowAllItems<T>() where T : WorkspaceViewModel
        {
            T workspace = this.Workspaces.FirstOrDefault(vm => vm is T) as T;
            if (workspace == null)
            {
                workspace = Activator.CreateInstance<T>() as T;
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllTowar()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieTowaryViewModel) as WszystkieTowaryViewModel;
            if (workspace is null)
            {
                workspace = new WszystkieTowaryViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private WszystkieTowaryViewModel showAllTowaryMessanger()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieTowaryViewModel) as WszystkieTowaryViewModel;
            if (workspace is null)
            {
                workspace = new WszystkieTowaryViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
            return workspace;
        }

        private void showAllGmina()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieGminyViewModel) as WszystkieGminyViewModel;
            if (workspace is null)
            {
                workspace = new WszystkieGminyViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private void showAllKraj()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieKrajeViewModel) as WszystkieKrajeViewModel;
            if (workspace is null)
            {
                workspace = new WszystkieKrajeViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private void showAllMiasto()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieMiastaViewModel) as WszystkieMiastaViewModel;
            if (workspace is null)
            {
                workspace = new WszystkieMiastaViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private void showAllPowiat()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkiePowiatyViewModel) as WszystkiePowiatyViewModel;
            if (workspace is null)
            {
                workspace = new WszystkiePowiatyViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }
        private void showAllWojewodztwo()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieWojewodztwaViewModel) as WszystkieWojewodztwaViewModel;
            if (workspace is null)
            {
                workspace = new WszystkieWojewodztwaViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private void showAllWyksztalcenie()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieWyksztalceniaViewModel) as WszystkieWyksztalceniaViewModel;
            if (workspace is null)
            {
                workspace = new WszystkieWyksztalceniaViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private void showAllKodCN()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieKodyCNViewModel) as WszystkieKodyCNViewModel;
            if (workspace is null)
            {
                workspace = new WszystkieKodyCNViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private void showAllKartyKredytowe()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieKartyKredytoweViewModel) as WszystkieKartyKredytoweViewModel;
            if (workspace is null)
            {
                workspace = new WszystkieKartyKredytoweViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private void showAllKategorie()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieKategorieViewModel) as WszystkieKategorieViewModel;
            if (workspace is null)
            {
                workspace = new WszystkieKategorieViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private void showAllProducenci()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszyscyProducenciViewModel) as WszyscyProducenciViewModel;
            if (workspace is null)
            {
                workspace = new WszyscyProducenciViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private void setActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(Workspaces.Contains(workspace));
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(Workspaces);
            if (collectionView != null)
            {
                collectionView.MoveCurrentTo(workspace);
            }
        }

        private void showAllFaktury()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieFakturyViewModel) as WszystkieFakturyViewModel;
            if (workspace is null)
            {
                workspace = new WszystkieFakturyViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private void showAllKontrahenci()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszyscyKontrahenciViewModel) as WszyscyKontrahenciViewModel;
            if (workspace is null)
            {
                workspace = new WszyscyKontrahenciViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private WszyscyKontrahenciViewModel showAllKontrahenciMessanger()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszyscyKontrahenciViewModel) as WszyscyKontrahenciViewModel;
            if (workspace is null)
            {
                workspace = new WszyscyKontrahenciViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
            return workspace;
        }

        private void showAllPracownicy()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszyscyPracownicyViewModel) as WszyscyPracownicyViewModel;
            if (workspace is null)
            {
                workspace = new WszyscyPracownicyViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
        }

        private WszyscyPracownicyViewModel showAllPracownicyMessanger()
        {
            var workspace = Workspaces.FirstOrDefault(vw => vw is WszyscyPracownicyViewModel) as WszyscyPracownicyViewModel;
            if (workspace is null)
            {
                workspace = new WszyscyPracownicyViewModel();
                Workspaces.Add(workspace);
            }
            setActiveWorkspace(workspace);
            return workspace;
        }

        private void showRaport()
        {
            RaportViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is RaportViewModel) as RaportViewModel;
            if (workspace == null)
            {
                workspace = new RaportViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        #endregion

        #region Komendy menu i paska narzędzi
        public ICommand NowyTowarCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyTowarViewModel()));
            }
        }

        public ICommand TowaryCommand
        {
            get
            {
                return new BaseCommand(showAllTowar);
            }
        }
        public ICommand GminyCommand
        {
            get
            {
                return new BaseCommand(showAllGmina);
            }
        }

        public ICommand NowaFakturaCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaFakturaViewModel()));
            }
        }

        public ICommand FakturyCommand
        {
            get
            {
                return new BaseCommand(showAllFaktury);
            }
        }

        public ICommand NowyKontrahentCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyKontrahentViewModel()));
            }
        }

        public ICommand KontrahenciCommand
        {
            get
            {
                return new BaseCommand(showAllKontrahenci);
            }
        }

        public ICommand NowyPracownikCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyPracownikViewModel()));
            }
        }

        public ICommand PracownicyCommand
        {
            get
            {
                return new BaseCommand(showAllPracownicy);
            }
        }
        #endregion

        #region WidocznośćMenuBocznego
        private string _WidocznoscMenuBocznego;
        public string WidocznoscMenuBocznego
        {
            get
            {
                return _WidocznoscMenuBocznego;
            }
            set
            {
                if (value != _WidocznoscMenuBocznego)
                {
                    _WidocznoscMenuBocznego = value;
                    OnPropertyChanged(() => WidocznoscMenuBocznego);
                }
            }
        }

        private void ZmienWidocznoscMenuBocznego()
        {
            WidocznoscMenuBocznego = WidocznoscMenuBocznego == "Visible" ? "Collapsed" : "Visible";
        }

        public ICommand ZmienWidocznoscMenuBocznegoCommand
        {
            get { return new BaseCommand(() => ZmienWidocznoscMenuBocznego()); }
        }
        #endregion
    }
}