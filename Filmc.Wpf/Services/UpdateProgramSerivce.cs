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
        public UpdateProgramSerivce()
        {

        }

        public void CheckUpdate()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            UpdateInfo? info = Updater.Module.Updater.GetLastUpdate(assembly);

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
            bool isUpdated = Updater.Module.Updater.UpdateUpdater();
            if (isUpdated == false)
            {
                MessageBox.Show("Is not updated updater");
            }
        }
    }
}
