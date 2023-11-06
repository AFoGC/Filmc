using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Filmc.Wpf.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeProfleWindow.xaml
    /// </summary>
    public partial class ChangeProfleWindow : Window
    {
        public ChangeProfleWindow()
        {
            InitializeComponent();
            Save = false;
            ChangeProfile = false;
        }

        public bool Save { get; private set; }
        public bool ChangeProfile { get; private set; }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void ExitForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle, 0x112, 0xf012, 0);
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_DontSave_Click(object sender, RoutedEventArgs e)
        {
            ChangeProfile = true;
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            Save = true;
            ChangeProfile = true;
            this.Close();
        }
    }
}
