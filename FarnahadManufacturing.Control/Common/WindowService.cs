using System.Collections.Generic;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.Window;

namespace FarnahadManufacturing.Control.Common
{
    public class WindowService
    {
        private static Dictionary<DialogUserControl, FmDialogWindow> _openDialogWindows =
            new Dictionary<DialogUserControl, FmDialogWindow>();

        public static void OpenUserControlDialog<T>(T userControl) where T : DialogUserControl
        {
            var newWindow = new FmDialogWindow();
            newWindow.Title = userControl.UserControlTitle ?? "صفحه جدید";
            newWindow.Content = userControl;
            _openDialogWindows.Add(userControl, newWindow);
            newWindow.ShowDialog();
        }

        public static void CloseUserControlDialogWindow(DialogUserControl userControl)
        {
            if (_openDialogWindows.ContainsKey(userControl))
            {
                var window = _openDialogWindows[userControl];
                window.Close();
            }
        }
    }
}
