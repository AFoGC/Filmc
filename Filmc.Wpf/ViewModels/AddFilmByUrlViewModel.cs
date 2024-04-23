﻿using Filmc.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.ViewModels
{
    public class AddFilmByUrlViewModel : AddEntityByUrlViewModel
    {
        public AddFilmByUrlViewModel(AddEntityByUrlService addEntityByUrlService) : base(addEntityByUrlService)
        {

        }

        protected async override Task AddByUrl()
        {
            IsCloseButtonEnabled = false;

            await AddEntityByUrlService.CreateFilmByUrl(Url);

            IsCloseButtonEnabled = true;
        }
    }
}