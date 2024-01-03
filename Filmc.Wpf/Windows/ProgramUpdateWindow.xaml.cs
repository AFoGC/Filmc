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
using System.Windows.Shapes;

namespace Filmc.Wpf.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProgramUpdateWindow.xaml
    /// </summary>
    public partial class ProgramUpdateWindow : Window
    {
        public bool Update { get; private set; }

        public ProgramUpdateWindow(UpdateInfo info)
        {
            InitializeComponent();
            Update = false;

            VersionTextBlock.Text = info.Tag;
            DescriptionTextBlock.Text = info.Description;
        }

        private void OnUpdateClick(object sender, RoutedEventArgs e)
        {
            Update = true;
            this.Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
