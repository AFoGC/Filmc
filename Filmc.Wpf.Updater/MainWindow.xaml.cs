using Filmc.Wpf.Updater.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Filmc.Wpf.Updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProgramsUpdater _programsUpdater;

        public MainWindow(ProgramsUpdater programsUpdater)
        {
            _programsUpdater = programsUpdater;
            InitializeComponent();
        }

        protected async override void OnActivated(EventArgs e)
        {
            await _programsUpdater.UpdateFilmc();
            App.Current.Shutdown();
            base.OnActivated(e);
        }
    }
}
