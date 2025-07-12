using Filmc.Entities.Entities;
using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Filmc.Wpf.ViewModels
{
    public class RecomendationMenuViewModel : BaseViewModel
    {
        public EntityFamily? Status { get; private set; }
        public FilmViewModel[]? RecomendedFilms { get; private set; }
        public BookViewModel[]? RecomendedBooks { get; private set; }

        public ICommand CloseMenuCommand { get; private set; }

        public RecomendationMenuViewModel()
        {
            Status = null;
            CloseMenuCommand = new RelayCommand(CloseMenu);
        }

        public void CloseMenu(object? obj)
        {
            CloseMenu();
        }

        public void OpenMenu(FilmViewModel[] films)
        {
            RecomendedFilms = films;
            RecomendedBooks = null;
            Status = EntityFamily.Films;
            OnPropertyChanged(nameof(RecomendedFilms));
            OnPropertyChanged(nameof(RecomendedBooks));
            OnPropertyChanged(nameof(Status));
        }

        public void OpenMenu(BookViewModel[] books)
        {
            RecomendedFilms = null;
            RecomendedBooks = books;
            Status = EntityFamily.Books;
            OnPropertyChanged(nameof(RecomendedFilms));
            OnPropertyChanged(nameof(RecomendedBooks));
            OnPropertyChanged(nameof(Status));
        }

        public void CloseMenu()
        {
            RecomendedFilms = null;
            RecomendedBooks = null;
            Status = null;
            OnPropertyChanged(nameof(RecomendedFilms));
            OnPropertyChanged(nameof(RecomendedBooks));
            OnPropertyChanged(nameof(Status));
        }
    }
}
