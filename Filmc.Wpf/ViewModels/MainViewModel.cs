﻿using Filmc.Wpf.Commands;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private readonly ExitWindowService _exitService;
        private readonly SettingsService _settingsService;

        private RelayCommand? saveSettingsCommand;
        private RelayCommand? saveAndExitCommand;
        private RelayCommand? keyDownCommand;

        public MainViewModel(FilmsMenuViewModel filmsMenuViewModel, BooksMenuViewModel booksMenuViewModel, 
               SettingsMenuViewModel settingsMenuViewModel, StatusBarViewModel statusBarViewModel,
               SettingsService settingsService, ExitWindowService exitService)
        {
            _filmsMenuViewModel = filmsMenuViewModel;
            _booksMenuViewModel = booksMenuViewModel;
            _settingsMenuViewModel = settingsMenuViewModel;

            _exitService = exitService;
            _settingsService = settingsService;

            StatusBarViewModel = statusBarViewModel;

            FilmsSelected = true;
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
            get
            {
                return saveSettingsCommand ??
                (saveSettingsCommand = new RelayCommand(obj =>
                {
                    _settingsService.SaveSettings();
                }));
            }
        }

        public RelayCommand SaveAndExitCommand
        {
            get
            {
                return saveAndExitCommand ??
                (saveAndExitCommand = new RelayCommand(obj =>
                {
                    Profile selectedProfile = _settingsService.ProfilesService.SelectedProfile;

                    if (selectedProfile.IsChangesSaved == false)
                    {
                        CancelEventArgs? e = obj as CancelEventArgs;
                        _exitService.ShowDialog();

                        if (_exitService.Save)
                            selectedProfile.SaveTables();

                        e.Cancel = !_exitService.Close;
                    }
                }));
            }
        }

        public RelayCommand KeyDownCommand
        {
            get
            {
                return keyDownCommand ??
                (keyDownCommand = new RelayCommand(obj =>
                {
                    KeyEventArgs e = obj as KeyEventArgs;
                    if (e.Key == Key.S && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                    {
                        _settingsService.ProfilesService.SelectedProfile.SaveTables();
                    }
                }));
            }
        }
    }
}
