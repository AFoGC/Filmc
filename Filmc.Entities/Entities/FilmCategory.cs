using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using Filmc.Entities.PropertyTypes;

namespace Filmc.Entities.Entities
{
    public partial class FilmCategory : BaseEntity
    {
        private int _id;
        private string _name = null!;
        private string _hideName = null!;

        public FilmCategory()
        {
            Name = String.Empty;
            HideName = String.Empty;
            Mark = new Mark();

            Films = new NotifyCollection<Film>();
        }

        public int Id 
        { 
            get => _id; 
            set { _id = value; OnPropertyChanged(); }
        }
        public string Name 
        { 
            get => _name; 
            set { _name = value; OnPropertyChanged(); }
        }
        public string HideName 
        { 
            get => _hideName; 
            set { _hideName = value; OnPropertyChanged(); }
        }
        internal int? RawMark
        {
            get => Mark.RawMark;
            set => Mark.RawMark = value;
        }

        public virtual Mark Mark { get; }

        public virtual NotifyCollection<Film> Films { get; }
        public virtual INotifyCollection<Film> CategoryFilms => Films;

        public void AddFilmInOrder(Film film)
        {
            if (film.CategoryId == null)
            {
                film.CategoryListId = Films.Count;
                film.CategoryId = this.Id;
            }
        }

        public void RemoveFilmInOrder(Film film)
        {
            if (Films.Remove(film))
            {
                var sortedFilms = Films.OrderBy(x => x.CategoryListId);

                int i = 0;
                foreach (Film item in sortedFilms)
                    item.CategoryListId = i++;
            }
        }

        public bool ChangeCategoryListId(Film film, int? newListId)
        {
            if (Films.Contains(film) && newListId != null)
            {
                if (newListId < 0)
                    newListId = 0;

                if (newListId >= Films.Count)
                    newListId = Films.Count - 1;

                int? currentListId = film.CategoryListId;

                while(film.CategoryListId != newListId)
                {
                    if (film.CategoryListId > newListId)
                    {
                        currentListId = film.CategoryListId - 1;
                        Films.First(x => x.CategoryListId == currentListId).CategoryListId++;
                        film.CategoryListId--;
                    }

                    if (film.CategoryListId < newListId)
                    {
                        currentListId = film.CategoryListId + 1;
                        Films.First(x => x.CategoryListId == currentListId).CategoryListId--;
                        film.CategoryListId++;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
