using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Services
{
    public class UpdateMenuService
    {
        private readonly UpdateMenuViewModel _menuViewModel;

        public UpdateMenuService(UpdateMenuViewModel menuViewModel)
        {
            _menuViewModel = menuViewModel;
        }

        //public event Action? MenuEntityChanged;

        public BaseEntityViewModel? CurrentEntityViewModel => _menuViewModel.CurrentEntityViewModel;

        public void OpenUpdateMenu(BaseEntityViewModel entityViewModel)
        {
            _menuViewModel.CurrentEntityViewModel = entityViewModel;
            //MenuEntityChanged?.Invoke();
        }

        public void CloseUpdateMenu()
        {
            _menuViewModel.CloseMenu();
            //MenuEntityChanged?.Invoke();
        }
    }
}
