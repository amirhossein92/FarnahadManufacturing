using System;
using System.Windows;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Control.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcSetPassword : DialogUserControl
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
