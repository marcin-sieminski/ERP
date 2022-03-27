﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Firma.Helpers;

namespace Firma.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
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
            new CommandViewModel("Towary", new BaseCommand(showAllTowar)),
            new CommandViewModel("Towary", new BaseCommand(() => createView(new NowyTowarViewModel()))),
            new CommandViewModel("Faktura", new BaseCommand(() => createView(new NowaFakturaViewModel()))),
            new CommandViewModel("Faktury", new BaseCommand(showAllFaktury)),
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
        WorkspaceViewModel workspace = sender as WorkspaceViewModel;
        this.Workspaces.Remove(workspace);
    }
    #endregion

    #region Funkcje pomocnicze

    private void createView(WorkspaceViewModel workspace)
    {
        Workspaces.Add(workspace);
        setActiveWorkspace(workspace);
    }

    private void showAllTowar()
    {
        WszystkieTowaryViewModel workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieTowaryViewModel) as WszystkieTowaryViewModel;
        if (workspace is null)
        {
            workspace = new WszystkieTowaryViewModel();
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
        WszystkieFakturyViewModel workspace = Workspaces.FirstOrDefault(vw => vw is WszystkieFakturyViewModel) as WszystkieFakturyViewModel;
        if (workspace is null)
        {
            workspace = new WszystkieFakturyViewModel();
            Workspaces.Add(workspace);
        }
        setActiveWorkspace(workspace);
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

    #endregion
}