using Filmc.Wpf.Helper;
using Filmc.Wpf.Updater.Module;
using Filmc.Wpf.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Filmc.Wpf.Services
{
    public class UpdateProgramSerivce
    {
        private readonly ProgramsUpdater _programsUpdater;

        public UpdateProgramSerivce()
        {
            _programsUpdater = new ProgramsUpdater(PathHelper.MainDirectory);
        }

        public void CheckUpdate()
        {
            UpdateInfo? info = _programsUpdater.GetLastUpdate();

            if (info != null)
            {
                ProgramUpdateWindow window = new ProgramUpdateWindow(info);
                window.ShowDialog();

                if (window.Update)
                {
                    Process.Start(PathHelper.UpdaterProgramPath);
                    App.Current.Shutdown();
                }
            }
        }

        public void UpdateUpdater(string[] args)
        {
            if (args.Contains("-uu"))
            {
                Task.Run(UpdateUpdater);
            }
        }

        private void UpdateUpdater()
        {
            bool isUpdated = _programsUpdater.UpdateUpdater();
            if (isUpdated == false)
            {
                MessageBox.Show("Is not updated updater");
            }
        }
    }
}
