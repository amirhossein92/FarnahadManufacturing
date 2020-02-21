using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Xpf.Core;

namespace FarnahadManufacturing.UI.Common
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
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button3,
                MessageBoxOptions.RtlReading);

            if ((msgResult == DialogResult.Cancel) == false)
                result = msgResult == DialogResult.Yes;

            return result;
        }

        private static bool ShowQuestionYesNoMessageBox(string message, string title)
        {
            return MessageBox.Show(
                       message,
                       title,
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning,
                       MessageBoxDefaultButton.Button2,
                       MessageBoxOptions.RtlReading) == DialogResult.Yes;
        }

        private static void ShowInfoMessageBox(string message, string title)
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading);
        }
    }
}
