﻿using Filmc.Wpf.Commands;
using Filmc.Wpf.SettingsServices;
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
        private readonly SettingsMenuViewModel _settingsMenuViewModel;

        private RelayCommand saveSettingsCommand;

        public MainViewModel(FilmsMenuViewModel filmsMenuViewModel, BooksMenuViewModel booksMenuViewModel, 
               SettingsMenuViewModel settingsMenuViewModel, StatusBarViewModel statusBarViewModel,
               SettingsService settingsService)
        {
            _filmsMenuViewModel = filmsMenuViewModel;
            _booksMenuViewModel = booksMenuViewModel;
            _settingsMenuViewModel = settingsMenuViewModel;

            StatusBarViewModel = statusBarViewModel;

            FilmsSelected = true;

            saveSettingsCommand = new RelayCommand(obj => settingsService.SaveSettings());
        }

        public StatusBarViewModel StatusBarViewModel { get; }

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
                    CurrentMenu = _settingsMenuViewModel;
                }
                OnPropertyChanged();
            }
        }

        public BaseViewModel? CurrentMenu
        {
            get => _currentMenu;
            set { _currentMenu = value; OnPropertyChanged(); }
        }

        public RelayCommand SaveSettingsCommand
        {
            get => saveSettingsCommand;
        }
    }
}
