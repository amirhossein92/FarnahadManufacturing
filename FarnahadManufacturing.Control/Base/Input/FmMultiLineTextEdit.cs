using System.Windows;

// CHECK
namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmMultiLineTextEdit : FmTextEdit
    {
        public FmMultiLineTextEdit()
        {
            Height = 60;
            TextWrapping = TextWrapping.Wrap;
            AcceptsReturn = true;
            VerticalContentAlignment = VerticalAlignment.Top;
        }
    }
}
