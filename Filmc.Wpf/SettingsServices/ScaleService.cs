using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.SettingsServices
{
    public class ScaleService
    {
        public event Action<ScaleEnum>? ScaleChanged;

        public ScaleEnum CurrentScale { get; private set; }

        public ScaleService()
        {
            CurrentScale = ScaleEnum.Medium;
        }

        public void SetScale(ScaleEnum scale)
        {
            CurrentScale = scale;
            ScaleChanged?.Invoke(scale);
        }

        public void SetScale(int scaleCode)
        {
            SetScale((ScaleEnum)scaleCode);
        }
    }
}
