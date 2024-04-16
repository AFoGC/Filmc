using Filmc.Wpf.Models;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
using Filmc.Wpf.ViewModels;
using Filmc.Wpf.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Filmc.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            if (CheckExeIfExist())
                return;
                

            IServiceCollection serviceCollection = CreateServiceCollection();
            _serviceProvider = serviceCollection.BuildServiceProvider();

            this.DispatcherUnhandledException += OnDispatcherUnhandledException;

            InitializeComponent();
        }

        private IServiceCollection CreateServiceCollection()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ExplorerService>();
            serviceCollection.AddSingleton<ImportFileDialogService>();
            serviceCollection.AddSingleton<ProfilesService>();
            serviceCollection.AddSingleton<ExitWindowService>();
            serviceCollection.AddSingleton<ChangeProfileWindowService>();
            serviceCollection.AddSingleton<AddEntityByUrlService>();
            serviceCollection.AddSingleton<AddEntityWindowService>();

            serviceCollection.AddSingleton<AutoSaveService>();
            serviceCollection.AddSingleton<BackgroundImageService>();
            serviceCollection.AddSingleton<LanguageService>();
            serviceCollection.AddSingleton<MarkSystemService>();
            serviceCollection.AddSingleton<ScaleService>();
            serviceCollection.AddSingleton<GlobalSettingsService>();
            serviceCollection.AddSingleton<UpdateMenuService>();
            serviceCollection.AddSingleton<UpdateProgramSerivce>();

            serviceCollection.AddSingleton<FilmsModel>();
            serviceCollection.AddSingleton<BooksModel>();

            serviceCollection.AddSingleton<FilmsMenuViewModel>();
            serviceCollection.AddSingleton<BooksMenuViewModel>();
            serviceCollection.AddSingleton<SettingsMenuViewModel>();
            serviceCollection.AddSingleton<UpdateMenuViewModel>();

            serviceCollection.AddSingleton<StatusBarViewModel>();
            serviceCollection.AddSingleton<MainViewModel>();

            serviceCollection.AddSingleton(s => new MainWindow
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            return serviceCollection;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            GlobalSettingsService settingsService = _serviceProvider.GetRequiredService<GlobalSettingsService>();
            LanguageService languageService = _serviceProvider.GetRequiredService<LanguageService>();
            ScaleService scaleService = _serviceProvider.GetRequiredService<ScaleService>();

            UpdateProgramSerivce updateProgramSerivce = _serviceProvider.GetRequiredService<UpdateProgramSerivce>();

            languageService.LanguageChanged += OnLanguageChanged;
            scaleService.ScaleChanged += OnScaleChanged;

            updateProgramSerivce.UpdateUpdater(e.Args);

            settingsService.LoadSettings();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnScaleChanged(ScaleEnum scale)
        {
            string nameStart = "Resources/Dictionaries/TableControls/Scale.";
            ReplaceSource(nameStart, scale.ToString());
        }

        private void OnLanguageChanged(CultureInfo language)
        {
            string nameStart = "Resources/Localizations/lang.";
            ReplaceSource(nameStart, language.ToString());
        }

        private void ReplaceSource(string resourceNameStart, string resourceNameEnd)
        {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri(resourceNameStart + resourceNameEnd + ".xaml", UriKind.Relative);

            ResourceDictionary oldDict =
                (from d in Resources.MergedDictionaries
                 where d.Source != null && d.Source.OriginalString.StartsWith(resourceNameStart)
                 select d).First();

            if (oldDict != null)
            {
                int ind = Resources.MergedDictionaries.IndexOf(oldDict);
                Resources.MergedDictionaries.Remove(oldDict);
                Resources.MergedDictionaries.Insert(ind, dict);
            }
            else
            {
                Resources.MergedDictionaries.Add(dict);
            }
        }

        private bool CheckExeIfExist()
        {
            string assemblyName = System.Reflection.Assembly.GetEntryAssembly().Location;
            string processName = System.IO.Path.GetFileNameWithoutExtension(assemblyName);
            return System.Diagnostics.Process.GetProcessesByName(processName).Count() > 1;
        }
    }
}
