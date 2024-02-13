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
            if (Films.Contains(film) == false)
            {
                film.CategoryListId = Films.Count;
                Films.Add(film);
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

                int oldListId = (int)film.CategoryListId!;

                var plusCollection = Films
                    .Where(x => x.CategoryListId >= newListId && x.Id != film.Id)
                    .OrderBy(x => x.CategoryListId);

                var minusCollection = Films
                    .Where(x => x.CategoryListId < newListId && x.Id != film.Id)
                    .OrderByDescending(x => x.CategoryListId);

                film.CategoryListId = newListId;

                int i = (int)newListId;
                foreach (Film item in plusCollection)
                {
                    i++;
                    item.CategoryListId = i;
                }

                i = (int)newListId;
                foreach (Film item in minusCollection)
                {
                    i--;
                    item.CategoryListId = i;
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
