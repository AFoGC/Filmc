using Filmc.SitesIntegration;
using Filmc.Wpf.Services;
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
        private readonly AddEntityByUrlService _addEntityByUrlService;
        private DetailedStatus _status;

        public AddEntityByUrlWindow(AddEntityByUrlService addEntityByUrlService)
        {
            InitializeComponent();

            _addEntityByUrlService = addEntityByUrlService;
        }

        public void SetAddFilm()
        {
            _status = DetailedStatus.IsFilm;
        }

        public void SetAddBook()
        {
            _status = DetailedStatus.IsBook;
        }

        public bool IsUrlWrited { get; private set; }
        public string Url => UrlText.Text;

        private void AddEntity(object sender, RoutedEventArgs e)
        {
            if (_status == DetailedStatus.IsFilm)
                _addEntityByUrlService.CreateFilmByUrl(Url);

            if (_status == DetailedStatus.IsBook)
                _addEntityByUrlService.CreateBookByUrl(Url);

            this.Close();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
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
