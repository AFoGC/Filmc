using Filmc.Wpf.ViewModels;
using Filmc.Wpf.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Filmc.Wpf.Services
{
    public class AddEntityWindowService
    {
        private readonly AddEntityByUrlService _addEntityByUrlService;

        public AddEntityWindowService(AddEntityByUrlService addEntityByUrlService)
        {
            _addEntityByUrlService = addEntityByUrlService;
        }

        public void OpenAddFilmWindow()
        {
            AddEntityByUrlWindow window = new AddEntityByUrlWindow();
            window.DataContext = new AddFilmByUrlViewModel(_addEntityByUrlService);
            window.ShowDialog();
        }

        public void OpenAddBookWindow()
        {
            AddEntityByUrlWindow window = new AddEntityByUrlWindow();
            window.DataContext = new AddBookByUrlViewModel(_addEntityByUrlService);
            window.ShowDialog();
        }
    }
}
