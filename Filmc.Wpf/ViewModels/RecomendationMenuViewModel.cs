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
using System.Windows;
using System.Windows.Input;

namespace Filmc.Wpf.ViewModels
{
    public class RecomendationMenuViewModel : BaseViewModel
    {
        public RecomendationMenuViewModel()
        {
            Status = null;
            CloseMenuCommand = new RelayCommand(CloseMenu);
        }

        public EntityFamily? Status { get; private set; }
        public ItemSimilarity<FilmViewModel>[]? RecomendedFilms { get; private set; }
        public ItemSimilarity<BookViewModel>[]? RecomendedBooks { get; private set; }

        public ICommand CloseMenuCommand { get; private set; }

        public Visibility MenuVisibility
        {
            get
            {
                Visibility visibility = Visibility.Hidden;

                if (Status != null)
                    visibility = Visibility.Visible;

                return visibility;
            }
        }

        public void CloseMenu(object? obj)
        {
            CloseMenu();
        }

        public void OpenMenu(ItemSimilarity<FilmViewModel>[] films)
        {
            RecomendedFilms = films;
            RecomendedBooks = null;
            Status = EntityFamily.Films;
            OnPropertyChanged(nameof(RecomendedFilms));
            OnPropertyChanged(nameof(RecomendedBooks));
            OnPropertyChanged(nameof(Status));
            OnPropertyChanged(nameof(MenuVisibility));
        }

        public void OpenMenu(ItemSimilarity<BookViewModel>[] books)
        {
            RecomendedFilms = null;
            RecomendedBooks = books;
            Status = EntityFamily.Books;
            OnPropertyChanged(nameof(RecomendedFilms));
            OnPropertyChanged(nameof(RecomendedBooks));
            OnPropertyChanged(nameof(Status));
            OnPropertyChanged(nameof(MenuVisibility));
        }

        public void CloseMenu()
        {
            RecomendedFilms = null;
            RecomendedBooks = null;
            Status = null;
            OnPropertyChanged(nameof(RecomendedFilms));
            OnPropertyChanged(nameof(RecomendedBooks));
            OnPropertyChanged(nameof(Status));
            OnPropertyChanged(nameof(MenuVisibility));
        }
    }
}
