using Filmc.Xtl.Entities;
using Filmc.Xtl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filmc.Wpf.Services;

namespace Filmc.Wpf.Models
{
    public class BooksModel
    {
        private readonly ProfilesService _profilesModel;


        public BooksModel(ProfilesService profilesModel)
        {
            _profilesModel = profilesModel;
            _profilesModel.SelectedProfileChanged += OnSelectedProfileChanged;
        }

        public TablesContext TablesContext => _profilesModel.SelectedProfile.TablesContext;

        public event Action? TablesContextChanged;

        private void OnSelectedProfileChanged(Profile profile)
        {
            TablesContextChanged?.Invoke();
        }

        public void AddCategory()
        {
            TablesContext.BookCategories.Add();
        }

        public void RemoveCategory(BookCategory category)
        {
            TablesContext.BookCategories.Remove(category);
        }

        public void AddBook()
        {
            Book book = new Book();
            book.GenreId = TablesContext.BookGenres.First().Id;
            TablesContext.Books.Add(book);
        }

        public void AddBook(BookCategory category)
        {
            Book book = new Book();
            book.GenreId = TablesContext.BookGenres.First().Id;

            if (category.HideName != String.Empty)
            {
                book.Name = category.HideName;
            }
            else
            {
                book.Name = category.Name;
            }

            TablesContext.Books.Add(book);

            TablesContext.BookCategories
                .First(x => x.Id == category.Id)
                .AddBookInOrder(book);
        }

        public void SaveTables()
        {
            _profilesModel.SelectedProfile.SaveTables();
        }
    }
}
