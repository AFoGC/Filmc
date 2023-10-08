using Filmc.Wpf.ViewModels;
using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override bool SetFinded(string search)
        {
            throw new NotImplementedException();
        }

        private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            
        }
    }
}
