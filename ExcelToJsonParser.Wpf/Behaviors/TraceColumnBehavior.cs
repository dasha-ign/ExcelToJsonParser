using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExcelToJsonParser.Wpf.Behaviors
{
    public class TraceColumnBehavior : Behavior<DataGrid>
    {
        #region ColumnIndex : int? - Курсор

        /// <summary>Курсор</summary>
        public static readonly DependencyProperty ColumnIndexProperty =
            DependencyProperty.Register(
                nameof(ColumnIndex),
                typeof(int?),
                typeof(TraceColumnBehavior),
                new PropertyMetadata(default(int?)));

        /// <summary>Курсор</summary>
        //[Category("")]
        [Description("Текущая колонка")]
        public int? ColumnIndex { get => (int?)GetValue(ColumnIndexProperty); set => SetValue(ColumnIndexProperty, value); }

        #endregion

        protected override void OnAttached()
        {
            AssociatedObject.MouseMove += OnMouseMove;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(AssociatedObject);
            
            var cell = AssociatedObject.SelectedCells[0];
            ColumnIndex = cell.Column.DisplayIndex;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= OnMouseMove;
        }


    }
}
