using Filmc.Xtl.Entities;
using Filmc.Xtl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Models
{
    public class BooksModel
    {
        private readonly ProfilesModel _profilesModel;


        public BooksModel(ProfilesModel profilesModel)
        {
            _profilesModel = profilesModel;
            _profilesModel.SelectedProfileChanged += OnSelectedProfileChanged;
        }

        public TablesContext TablesContext => _profilesModel.SelectedProfile.TablesContext;

        public event Action? TablesContextChanged;

        private void OnSelectedProfileChanged(ProfileModel profile)
        {
            TablesContextChanged?.Invoke();
        }

        public void AddCategory()
        {
            TablesContext.FilmCategories.Add();
        }

        public void RemoveCategory(BookCategory category)
        {
            TablesContext.BookCategories.Remove(category);
        }

        public void AddBook()
        {
            Film film = new Film();
            film.GenreId = TablesContext.FilmGenres.First().Id;
            TablesContext.Films.Add(film);
        }

        public void AddBook(int categoryId)
        {
            Book book = new Book();
            book.GenreId = TablesContext.BookGenres.First().Id;
            TablesContext.Books.Add(book);

            TablesContext.BookCategories
                .First(x => x.Id == categoryId)
                .AddBookInOrder(book);
        }

        public void SaveTables()
        {
            _profilesModel.SelectedProfile.SaveTables();
        }
    }
}
