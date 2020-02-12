using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.XtraPrinting.Drawing;
using ImageSource = System.Windows.Media.ImageSource;

namespace FarnahadManufacturing.UI.Common
{
    public static class ImageUtility
    {
        public static ImageSource CreateSvgImage(string relativePath)
        {
            var uri = new Uri($"pack://application:,,,/{relativePath}", UriKind.Absolute);
            var svgHelper = SvgImageHelper.CreateImage(uri);
            return WpfSvgRenderer.CreateImageSource(svgHelper, 1d, null, null, true);
        }

    }
}
