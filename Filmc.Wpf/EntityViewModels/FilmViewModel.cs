using Filmc.Wpf.ViewModels;
using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Filmc.Wpf.EntityViewModels
{
    public class FilmViewModel : BaseEntityViewModel
    {
        public Film Model { get; }

        

        public FilmViewModel(Film model)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;
        }

        public int Id
        {
            get => Model.Id;
            set => Model.Id = value;
        }
        public string Name
        {
            get => Model.Name;
            set => Model.Name = value;
        }
        public FilmGenre Genre
        {
            get => Model.Genre;
            set => Model.GenreId = value.Id;
        }
        public int RealiseYear
        {
            get => Model.RealiseYear;
            set => Model.RealiseYear = value;
        }
        public bool IsWatched
        {
            get => Model.IsWatched;
            set => Model.IsWatched = value;
        }
        public DateTime EndWatchDate
        {
            get => Model.EndWatchDate;
            set => Model.EndWatchDate = value;
        }
        public string Comment
        {
            get => Model.Comment;
            set => Model.Comment = value;
        }
        public int CountOfViews
        {
            get => Model.CountOfViews;
            set => Model.CountOfViews = value;
        }
        public int CategoryListId
        {
            get => Model.CategoryListId;
            set => Model.CategoryListId = value;
        }
        public DateTime StartWatchDate
        {
            get => Model.StartWatchDate;
            set => Model.StartWatchDate = value;
        }
        public int WatchedSeries
        {
            get => Model.WatchedSeries;
            set => Model.WatchedSeries = value;
        }
        public int TotalSeries
        {
            get => Model.TotalSeries;
            set => Model.TotalSeries = value;
        }

        

        public override bool Search(string search)
        {
            throw new NotImplementedException();
        }

        private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            
        }
    }
}
