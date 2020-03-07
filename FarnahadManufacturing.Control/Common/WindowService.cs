using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;
using FarnahadManufacturing.Control.Base.Layout;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.Window;

namespace FarnahadManufacturing.Control.Common
{
    public class WindowService
    {
        private static Dictionary<DialogUserControlBase, FmDialogWindow> _openDialogWindows =
            new Dictionary<DialogUserControlBase, FmDialogWindow>();

        public static void OpenUserControlDialog<T>(T userControl) where T : DialogUserControlBase
        {
            var newWindow = new FmDialogWindow();
            newWindow.Title = userControl.UserControlTitle ?? "صفحه جدید";
            newWindow.Content = userControl;
            _openDialogWindows.Add(userControl, newWindow);
            newWindow.ShowDialog();
        }

        public static void CloseUserControlDialogWindow(DialogUserControlBase userControl)
        {
            if (_openDialogWindows.ContainsKey(userControl))
            {
                var window = _openDialogWindows[userControl];
                window.Close();
            }
        }
    }
}
