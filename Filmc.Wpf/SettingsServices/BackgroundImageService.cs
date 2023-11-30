using Filmc.Wpf.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Filmc.Wpf.SettingsServices
{
    public class BackgroundImageService
    {
        private BitmapImage? _image;
        private double _opacity;
        private string? _imageName;

        public BackgroundImageService()
        {
            Opacity = 0.1;
        }

        public event Action<BitmapImage?>? ImageChanged;
        public event Action<double>? OpacityChanged;

        public BitmapImage? Image
        {
            get => _image;
            private set
            {
                _image = value;
                ImageChanged?.Invoke(_image);
                GC.Collect();
            }
        }

        public double Opacity
        {
            get => _opacity;
            set
            {
                _opacity = value;
                OpacityChanged?.Invoke(_opacity);
            }
        }

        public string? ImageName
        {
            get => _imageName;
            set
            {
                _imageName = value;
                if (_imageName != null)
                {
                    string path = Path.Combine(PathHelper.ImagesResourcePath, _imageName);

                    BitmapImage bi = new BitmapImage();
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        bi.BeginInit();
                        bi.StreamSource = fs;
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.EndInit();
                    }

                    bi.Freeze();

                    Image = bi;
                }
                else
                {
                    Image = null;
                }
            }
        }

        public void SetNewImage(string? path)
        {
            if (Directory.Exists(PathHelper.ImagesResourcePath) == false)
                Directory.CreateDirectory(PathHelper.ImagesResourcePath);

            if (path != null)
            {
                ImageName = null;

                string extension = Path.GetExtension(path);
                string imageName = $"MainImage{extension}";

                string newFilePath = Path.Combine(PathHelper.ImagesResourcePath, imageName);
                File.Copy(path, newFilePath, true);

                ImageName = imageName;
            }
            else
            {
                ImageName = null;
            }
        }
    }
}
