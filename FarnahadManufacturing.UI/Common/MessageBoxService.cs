using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.Filtering.Helpers;

namespace FarnahadManufacturing.UI.Common
{
    public static class MessageBoxService
    {
        public static DialogResult AskForDelete(string title = null)
        {
            var msg = "آیا از حذف شدن اطمینان دارید؟";
            if (!string.IsNullOrEmpty(title))
                msg = $"آیا از حذف شدن '{title}' اطمینان دارید؟";
            return MessageBox.Show(
                msg,
                "حذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);
        }

        public static void SaveConfirmation(string title = null)
        {
            var msg = "ذخیره با موفقیت انجام شد";
            if (!string.IsNullOrEmpty(title))
                msg = $"ذخیره '{title}' با موفقیت انجام شد";
            MessageBox.Show(
                msg,
                "ذخیره",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

        }
    }
}
