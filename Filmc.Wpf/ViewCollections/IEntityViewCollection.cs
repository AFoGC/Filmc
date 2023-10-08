using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.ViewCollections
{
    public interface IEntityViewCollection
    {
        ICollectionView View { get; }
        void ChangeSortProperty(string propertyName);
    }
}
