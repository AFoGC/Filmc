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
    /// Логика взаимодействия для AddEntityByUrlWindow.xaml
    /// </summary>
    public partial class AddEntityByUrlWindow : Window
    {
        public AddEntityByUrlWindow()
        {
            InitializeComponent();
        }

        public bool IsUrlWrited { get; private set; }
        public string Url => UrlText.Text;

        private void AddEntity(object sender, RoutedEventArgs e)
        {
            IsUrlWrited = true;
            this.Close();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            IsUrlWrited = false;
            this.Close();
        }
    }
}
