using FarnahadManufacturing.Control.Base.Layout;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.Window;

namespace FarnahadManufacturing.Control.Common
{
    public class WindowService
    {
        public static void OpenUserControlDialog<T>(T userControl) where T : DialogUserControlBase
        {
            var newWindow = new FmDialogWindow();
            newWindow.Title = userControl.UserControlTitle;
            var grid = new FmGrid();
            grid.Children.Add(userControl);
            newWindow.Content = grid;
            newWindow.ShowDialog();
        }
    }
}
