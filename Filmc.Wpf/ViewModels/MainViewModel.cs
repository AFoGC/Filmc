using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool? _filmsSelected;
        private bool? _booksSelected;
        private bool? _settingsSelected;

        private BaseViewModel? _currentMenu;

        private readonly FilmsMenuViewModel _filmsMenuViewModel;
        private readonly BooksMenuViewModel _booksMenuViewModel;

        public MainViewModel(FilmsMenuViewModel filmsMenuViewModel, BooksMenuViewModel booksMenuViewModel)
        {
            _filmsMenuViewModel = filmsMenuViewModel;
            _booksMenuViewModel = booksMenuViewModel;

            FilmsSelected = true;
        }

        public bool? FilmsSelected
        {
            get => _filmsSelected;
            set 
            { 
                _filmsSelected = value;
                if (_filmsSelected == true)
                {
                    BooksSelected = false;
                    SettingsSelected = false;
                    CurrentMenu = _filmsMenuViewModel;
                }
                OnPropertyChanged();
            }
        }

        public bool? BooksSelected
        {
            get => _booksSelected;
            set 
            { 
                _booksSelected = value;
                if (_booksSelected == true)
                {
                    FilmsSelected = false;
                    SettingsSelected = false;
                    CurrentMenu = _booksMenuViewModel;
                }
                OnPropertyChanged(); 
            }
        }

        public bool? SettingsSelected
        {
            get => _settingsSelected;
            set
            {
                _settingsSelected = value;
                if (_settingsSelected == true)
                {
                    FilmsSelected = false;
                    BooksSelected = false;
                    CurrentMenu = null;
                }
                OnPropertyChanged();
            }
        }

        public BaseViewModel? CurrentMenu
        {
            get => _currentMenu;
            set { _currentMenu = value; OnPropertyChanged(); }
        }
    }
}
