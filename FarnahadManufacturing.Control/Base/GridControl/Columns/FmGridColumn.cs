using System;
using System.Windows;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Control.Base.Size;

namespace FarnahadManufacturing.Control.Base.GridControl.Columns
{
    public class FmGridColumn : GridColumn
    {
        public FmGridColumn()
        {
            FmGridColumnWidth = FmGridColumnWidth.Auto;
        }

        public static readonly DependencyProperty FmGridColumnWidthProperty = DependencyProperty.Register(
            "FmGridColumnWidth", typeof(FmGridColumnWidth), typeof(FmGridColumn), new PropertyMetadata(default(FmGridColumnWidth), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var column = (FmGridColumn)d;
            if (e.NewValue is FmGridColumnWidth gridColumnWidth)
            {
                switch (gridColumnWidth)
                {
                    case FmGridColumnWidth.Auto:
                        if (column.View is TableView tableView)
                            tableView.BestFitColumn(column);
                        break;
                    case FmGridColumnWidth.x:
                        column.Width = new GridColumnWidth(1, GridColumnUnitType.Star);
                        break;
                    case FmGridColumnWidth.x2:
                        column.Width = new GridColumnWidth(2, GridColumnUnitType.Star);
                        break;
                    case FmGridColumnWidth.x3:
                        column.Width = new GridColumnWidth(3, GridColumnUnitType.Star);
                        break;
                    default:
                        var value = Convert.ToDouble(e.NewValue);
                        column.Width = new GridColumnWidth(value, GridColumnUnitType.Pixel);
                        break;
                }
            }
        }

        public FmGridColumnWidth FmGridColumnWidth
        {
            get => (FmGridColumnWidth)GetValue(FmGridColumnWidthProperty);
            set => SetValue(FmGridColumnWidthProperty, value);
        }
    }
}
