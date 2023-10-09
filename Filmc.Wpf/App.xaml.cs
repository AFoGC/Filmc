using Filmc.Wpf.Models;
using Filmc.Wpf.ViewModels;
using Filmc.Wpf.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Filmc.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ProfilesModel profiles = new ProfilesModel();
            FilmsModel filmsModel = new FilmsModel(profiles);
            FilmsViewModel filmsViewModel = new FilmsViewModel(filmsModel);

            MainWindow = new MainWindow();
            MainWindow.DataContext = filmsViewModel;
            MainWindow.Show();
        }
    }
}
