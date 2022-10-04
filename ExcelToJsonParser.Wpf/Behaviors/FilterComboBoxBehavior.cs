using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ExcelToJsonParser.Wpf.Behaviors
{
    public class FilterComboBoxBehavior : Behavior<ComboBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.KeyUp += ComboBoxKeyUp; 
        }

        private void ComboBoxKeyUp(object sender, KeyEventArgs e)
        {
            var combobox = (ComboBox)sender;
            combobox.IsDropDownOpen = true;
           CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(AssociatedObject.ItemsSource);

            itemsViewOriginal.Filter = ((o) =>
            {
                if (String.IsNullOrEmpty(combobox.Text)) return true;
                else
                {
                    return o.ToString().Contains(combobox.Text);
                }
            });

            itemsViewOriginal.Refresh();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.KeyUp -= ComboBoxKeyUp;
        }
    }
}
