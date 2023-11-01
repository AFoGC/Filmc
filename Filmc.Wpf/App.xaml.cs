using Filmc.Wpf.Models;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
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
            ProfilesService profiles = new ProfilesService();
            FilmsModel fmodel = new FilmsModel(profiles);
            FilmsMenuViewModel fviewModel = new FilmsMenuViewModel(fmodel);

            BooksModel bmodel = new BooksModel(profiles);
            BooksMenuViewModel bviewModel = new BooksMenuViewModel(bmodel);

            AutoSaveService autoSaveService = new AutoSaveService(profiles);
            LanguageService languageService = new LanguageService();
            MarkSystemService markSystemService = new MarkSystemService(profiles);
            ScaleService scaleService = new ScaleService();

            SettingsService settingsService = new SettingsService(profiles, autoSaveService, languageService, scaleService);
            SettingsMenuViewModel settingsMenuViewModel = new SettingsMenuViewModel(settingsService, markSystemService);

            MainViewModel mainViewModel = new MainViewModel(fviewModel, bviewModel, settingsMenuViewModel);

            MainWindow = new MainWindow();
            MainWindow.DataContext = mainViewModel;
            MainWindow.Show();
        }
    }
}
