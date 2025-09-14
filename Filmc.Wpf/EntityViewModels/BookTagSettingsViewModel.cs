using Filmc.Entities.Entities;
using Filmc.Wpf.Commands;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.EntityViewModels
{
    public class BookTagSettingsViewModel : BaseViewModel
    {
        private readonly BookTag _model;
        private readonly IEnumerable<BookTagCategorySettingsViewModel> _categories;
        private readonly IRepositoriesSaved _repositories;

        public BookTagSettingsViewModel(BookTag model, IRepositoriesSaved repositories, IEnumerable<BookTagCategorySettingsViewModel> categories)
        {
            _model = model;
            _repositories = repositories;
            _categories = categories;

            _model.PropertyChanged += OnModelPropertyChanged;
            RemoveTagCategoryCommand = new RelayCommand(RemoveTagCategory);
        }

        public BookTag Model => _model;

        public int Id
        {
            get => _model.Id;
        }

        public string Name
        {
            get => _model.Name;
            set => _model.Name = value;
        }

        public BookTagCategorySettingsViewModel? Category
        {
            get => _categories.FirstOrDefault(x => x.Id == _model.CategoryId);
            set
            {
                if (value != null)
                {
                    _model.Category = value.Model;
                }
                else
                {
                    _model.Category = null;
                }
                _repositories.SaveChanges();
            }
        }

        public RelayCommand RemoveTagCategoryCommand { get; }

        private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        private void RemoveTagCategory(object? obj)
        {
            Category = null;
        }
    }
}
