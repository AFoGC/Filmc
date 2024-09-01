using Filmc.Entities.PropertyTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Entities.Entities
{
    public class FilmWatchProgress : BaseEntity
    {
        private int _id;
        private string _name = null!;

        public FilmWatchProgress()
        {
            _id = 0;
            _name = String.Empty;

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

        public virtual NotifyCollection<Film> Films { get; }
    }
}
