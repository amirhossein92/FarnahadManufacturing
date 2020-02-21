using System.Windows;

namespace FarnahadManufacturing.Control.Common
{
    public static class MessageBoxService
    {
        public static bool AskForClose()
        {
            var msg = "آیا از بسته شدن برنامه اطمینان دارید؟";
            return ShowQuestionYesNoMessageBox(msg, "بستن برنامه");
        }

        public static bool? AskForDelete(string title = null)
        {
            var msg = "آیا از حذف شدن اطمینان دارید؟";
            if (!string.IsNullOrEmpty(title))
                msg = $"آیا از حذف شدن '{title}' اطمینان دارید؟";
            return ShowQuestionYesNoCancelMessageBox(msg, "حذف");
        }

        public static void SaveConfirmation(string title = null)
        {
            var msg = "ذخیره با موفقیت انجام شد";
            if (!string.IsNullOrEmpty(title))
                msg = $"ذخیره '{title}' با موفقیت انجام شد";

            ShowInfoMessageBox(msg, "ذخیره");
        }

        private static bool? ShowQuestionYesNoCancelMessageBox(string message, string title)
        {
            bool? result = null;

            var msgResult = MessageBox.Show(
                message,
                title,
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Warning,
                MessageBoxResult.Cancel,
                MessageBoxOptions.RtlReading);

            if ((msgResult == MessageBoxResult.Cancel) == false)
                result = msgResult == MessageBoxResult.Yes;

            return result;
        }

        private static bool ShowQuestionYesNoMessageBox(string message, string title)
        {
            return MessageBox.Show(
                       message,
                       title,
                       MessageBoxButton.YesNo,
                       MessageBoxImage.Warning,
                       MessageBoxResult.No,
                       MessageBoxOptions.RtlReading) == MessageBoxResult.Yes;
        }

        private static void ShowInfoMessageBox(string message, string title)
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButton.OK,
                MessageBoxImage.Information,
                MessageBoxResult.OK,
                MessageBoxOptions.RtlReading);
        }
    }
}
