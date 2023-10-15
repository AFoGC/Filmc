using Filmc.Xtl.EntityProperties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class FilmCategory : Record
    {
        private int _id;
        private string _name;
        private string _hideName;
        private int _priority;

        private Mark _mark;

        private readonly RecordsCollection<Film> _films;

        public FilmCategory()
        {
            _id = 0;
            _name = String.Empty;
            _hideName = String.Empty;
            _priority = 0;

            _mark = new Mark();

            _films = new RecordsCollection<Film>();
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
        public int Priority 
        { 
            get => _priority; 
            set { _priority = value; OnPropertyChanged(); }
        }

        internal int RawMark
        {
            get => Mark.RawMark;
            set => Mark.RawMark = value;
        }
        public Mark Mark 
        { 
            get => _mark;
            private set => _mark = value;
        }

        public RecordsCollection<Film> Films => _films;

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

        public bool ChangeCategoryListId(Film film, int newListId)
        {
            if (Films.Contains(film))
            {
                var plusCollection = Films
                    .Where(x => x.CategoryListId >= newListId && x.CategoryListId < film.CategoryListId)
                    .OrderBy(x => x.CategoryListId);

                var minusCollection = Films
                    .Where(x => x.CategoryListId <= newListId && x.CategoryListId > film.CategoryListId)
                    .OrderBy(x => x.CategoryListId);

                foreach (Film item in plusCollection)
                    item.CategoryListId++;

                foreach (Film item in minusCollection)
                    item.CategoryListId--;

                if (newListId < 0)
                    newListId = 0;

                if (newListId >= Films.Count)
                    newListId = Films.Count - 1;

                film.CategoryListId = newListId;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override object Clone()
        {
            FilmCategory category = new FilmCategory();

            category._id = _id;
            category._name = _name;
            category._hideName = _hideName;
            category._priority = _priority;

            category._mark.RawMark = _mark.RawMark;

            return category;
        }
    }
}
