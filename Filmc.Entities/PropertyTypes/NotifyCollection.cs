using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Entities.PropertyTypes
{
    public class NotifyCollection<T> : ObservableCollection<T>, INotifyCollection<T>
    {
        
    }
}
