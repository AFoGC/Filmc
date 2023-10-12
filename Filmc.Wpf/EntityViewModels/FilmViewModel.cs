using Filmc.Wpf.ViewModels;
using Filmc.Xtl.Entities;
using Filmc.Xtl.EntityProperties;
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

        private bool _isCommentVisible;
        private bool _isSelected;

        public FilmViewModel(Film model)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;
            Model.Mark.PropertyChanged += OnModelPropertyChanged;

            _isCommentVisible = false;
            _isSelected = false;
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
        
        public int FormatedMark
        {
            get => Model.Mark.FormatedMark;
            set => Model.Mark.FormatedMark = value;
        }
        public int MarkSystem
        {
            get => Model.Mark.MarkSystem;
            set => Model.Mark.MarkSystem = value;
        }

        public bool IsCommentVisible
        {
            get => _isCommentVisible;
            set { _isCommentVisible = value; OnPropertyChanged(); }
        }
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }

        public override bool Search(string search)
        {
            throw new NotImplementedException();
        }

        private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
