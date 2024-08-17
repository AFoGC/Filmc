using Filmc.Entities.Entities;
using Filmc.Wpf.Services;
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
        public void AddSource(Profile profile);
        public void RemoveSource(ISource source, Profile profile);
        public void SetFirstSource(ISource source, Profile profile);
    }
}
