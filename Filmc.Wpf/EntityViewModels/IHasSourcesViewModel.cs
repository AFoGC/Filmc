using Filmc.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.EntityViewModels
{
    public interface IHasSourcesViewModel
    {
        public void AddSource();
        public void RemoveSource(ISource source);
        public void SetFirstSource(ISource source);
    }
}
