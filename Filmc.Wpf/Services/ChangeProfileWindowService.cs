﻿using Filmc.Wpf.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Services
{
    public class ChangeProfileWindowService
    {
        public ChangeProfileWindowService()
        {

        }

        public bool? ShowDialog()
        {
            ChangeProfleWindow window = new ChangeProfleWindow();

            bool? result = window.ShowDialog();

            Save = window.Save;
            ChangeProfile = window.ChangeProfile;

            return result;
        }

        public bool Save { get; private set; }
        public bool ChangeProfile { get; private set; }
    }
}
