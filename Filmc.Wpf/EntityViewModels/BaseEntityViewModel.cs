using Filmc.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.EntityViewModels
{
    public abstract class BaseEntityViewModel : BaseViewModel
    {
        private bool _isFiltered;
        private bool _isFinded;

        public BaseEntityViewModel()
        {
            _isFiltered = true;
            _isFinded = true;
        }

        public bool IsFiltered
        {
            get => _isFiltered;
            set { _isFiltered = value; OnPropertyChanged(); }
        }

        public bool IsFinded
        {
            get => _isFinded;
            set { _isFinded = value; OnPropertyChanged(); }
        }

        public abstract bool Search(string search);
        //public abstract bool PassFilter();
    }
}
