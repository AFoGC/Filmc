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
        public abstract bool SetFinded(string search);
    }
}
