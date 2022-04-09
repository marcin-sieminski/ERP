﻿using System.Windows;
using Firma.ViewModels;
using Firma.Views;

namespace Firma
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            window.DataContext = new MainWindowViewModel();
            window.Show();
        }
    }
}
