using Filmc.Wpf.Updater.Module;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Filmc.Wpf.Updater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ProgramsUpdater _programsUpdater;

        public App()
        {
            string updaterPath = Assembly.GetExecutingAssembly().Location;
            string updaterDirectory = Path.GetDirectoryName(updaterPath)!;
            string mainDirectory = Path.GetDirectoryName(updaterDirectory)!;

            _programsUpdater = new ProgramsUpdater(mainDirectory);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = new MainWindow(_programsUpdater);
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _programsUpdater.FilmcStartup();
            base.OnExit(e);
        }
    }
}
