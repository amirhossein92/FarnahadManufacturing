using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;

namespace FarnahadManufacturing.Control.Common
{
    public static class MessageBoxService
    {
        private static IMessageBoxService _messageBoxService = new DXMessageBoxService();

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

        public static void CannotEditPrompt(string title)
        {
            var msg = "امکان ویرایش به علت غیر قابل تغییر بودن وجود ندارد";
            if (!string.IsNullOrEmpty(title))
                msg = $"امکان ویرایش به علت غیر قابل تغییر بودن {title} وجود ندارد";
            ShowWarningMessageBox(msg, "غیر قابل تغییر");
        }

        public static void CannotDeleteParent(string title)
        {
            var msg = "امکان حذف به علت استفاده در گروه های دیگر وجود ندارد. ابتدا گروه های زیری باید حذف شوند.";
            if (!string.IsNullOrEmpty(title))
                msg = $"امکان حذف {title} به علت استفاده در گروه های دیگر وجود ندارد. ابتدا گروه های زیری باید حذف شوند.";
            ShowWarningMessageBox(msg, "غیر قابل حذف");
        }

        public static void ShowError(string message, string title)
        {
            ShowErrorMessageBox(message, title);
        }

        private static bool? ShowQuestionYesNoCancelMessageBox(string message, string title)
        {
            bool? result = null;

            var msgResult = _messageBoxService.Show(
                message,
                title,
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Warning,
                MessageBoxResult.Cancel);

            if ((msgResult == MessageBoxResult.Cancel) == false)
                result = msgResult == MessageBoxResult.Yes;

            return result;
        }

        private static bool ShowQuestionYesNoMessageBox(string message, string title)
        {
            return _messageBoxService.Show(
                       message,
                       title,
                       MessageBoxButton.YesNo,
                       MessageBoxImage.Warning,
                       MessageBoxResult.No) == MessageBoxResult.Yes;
        }

        private static void ShowInfoMessageBox(string message, string title)
        {
            _messageBoxService.Show(
                message,
                title,
                MessageBoxButton.OK,
                MessageBoxImage.Information,
                MessageBoxResult.OK);
        }

        private static void ShowWarningMessageBox(string message, string title)
        {
            _messageBoxService.Show(
                message,
                title,
                MessageBoxButton.OK,
                MessageBoxImage.Warning,
                MessageBoxResult.OK);
        }

        private static void ShowErrorMessageBox(string message, string title)
        {
            _messageBoxService.Show(
                message,
                title,
                MessageBoxButton.OK,
                MessageBoxImage.Error,
                MessageBoxResult.OK);
        }
    }
}
