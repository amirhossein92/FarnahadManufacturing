using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcSetPassword : DialogUserControlBase
    {
        public UcSetPassword()
        {
            InitializeComponent();
            UserControlTitle = "تغییر رمز عبور";
        }

        private int _activeUserId = 0;

        public UcSetPassword(int userId) : this()
        {
            _activeUserId = userId;
            if (_activeUserId <= 0)
            {
                CurrentPasswordTextEdit.IsEnabled = false;
            }
        }

        private void SaveButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (NewPasswordTextEdit.Text == RepeatNewPasswordTextEdit.Text)
            {
                if (_activeUserId > 0)
                {
                    using (var dbContext = new FarnahadManufacturingDbContext())
                    {
                        var user = dbContext.Users.Find(_activeUserId);
                        var password =
                            EncryptionUtility.EncryptPassword(CurrentPasswordTextEdit.Text, user.PasswordSalt);
                        if (user.Password == password)
                        {
                            ApplicationDataStore.SendData("CurrentPassword", password);
                            ApplicationDataStore.SendData("CurrentPasswordSalt", user.PasswordSalt);
                            CloseWindow();
                        }
                        else
                        {
                            MessageBox.Show("رمز عبور اشتباه است.");
                        }
                    }
                }
                else
                {
                    var passwordSalt = Guid.NewGuid().ToString();
                    var password = EncryptionUtility.EncryptPassword(NewPasswordTextEdit.Text, passwordSalt);
                    ApplicationDataStore.SendData("CurrentPassword", password);
                    ApplicationDataStore.SendData("CurrentPasswordSalt", passwordSalt);
                }
            }
            else
            {
                MessageBox.Show("رمز عبور غیر یکسان است.");
            }
        }

        private void CancelButtonOnClick(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            WindowService.CloseUserControlDialogWindow(this);
        }
    }
}
