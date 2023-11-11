using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.SettingsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Filmc.Wpf.ViewModels
{
    public class UpdateMenuViewModel : BaseViewModel
    {
        private readonly MarkSystemService _markSystemService;

        private BaseEntityViewModel? _currentEntityViewModel;
        private List<int?>? _filmMarks;
        private List<int?>? _bookMarks;

        private RelayCommand? closeMenuCommand;

        public UpdateMenuViewModel(MarkSystemService markSystemService)
        {
            _markSystemService = markSystemService;

            _markSystemService.FilmsMarkSystemChanged += OnFilmsMarkSystemChanged;
            _markSystemService.BooksMarkSystemChanged += OnBooksMarkSystemChanged;

            OnFilmsMarkSystemChanged();
            OnBooksMarkSystemChanged();
        }

        public bool IsVisible => _currentEntityViewModel != null;
        public Type? CurrentEntityViewModelType => CurrentEntityViewModel?.GetType();

        public List<int?>? FilmMarks
        {
            get => _filmMarks;
            set { _filmMarks = value; OnPropertyChanged(); }
        }

        public List<int?>? BookMarks
        {
            get => _bookMarks;
            set { _filmMarks = value; OnPropertyChanged(); }
        }

        public Visibility MenuVisibility
        {
            get
            {
                Visibility visibility = Visibility.Hidden;

                if (IsVisible)
                    visibility = Visibility.Visible;

                return visibility;
            }
        }

        public BaseEntityViewModel? CurrentEntityViewModel
        {
            get => _currentEntityViewModel;
            set
            { 
                _currentEntityViewModel = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsVisible));
                OnPropertyChanged(nameof(MenuVisibility));
            }
        }

        public RelayCommand CloseMenuCommand
        {
            get
            {
                return closeMenuCommand ??
                (closeMenuCommand = new RelayCommand(obj =>
                {
                    CloseMenu();
                }));
            }
        }

        private void OnFilmsMarkSystemChanged()
        {
            List<int?> marks = new List<int?>();

            int i = _markSystemService.FilmMarkSystem;

            while(i != 0)
            {
                marks.Add(i);
                i--;
            }

            marks.Add(null);
            FilmMarks = marks;
        }

        private void OnBooksMarkSystemChanged()
        {
            List<int?> marks = new List<int?>();

            int i = _markSystemService.BookMarkSystem;

            while (i != 0)
            {
                marks.Add(i);
                i--;
            }

            marks.Add(null);
            BookMarks = marks;
        }

        public void CloseMenu()
        {
            CurrentEntityViewModel = null;
        }
    }
}
