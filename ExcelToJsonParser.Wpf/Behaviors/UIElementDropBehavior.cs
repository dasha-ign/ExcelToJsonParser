using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;

namespace ExcelToJsonParser.Wpf.Behaviors
{
    public class UIElementDropBehavior : Behavior<UIElement>
    {
        private AdornerManager adornerManager;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AllowDrop = true;
            AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            AssociatedObject.DragOver += AssociatedObject_DragOver;
            AssociatedObject.DragLeave += AssociatedObject_DragLeave;
            AssociatedObject.Drop += AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (adornerManager != null)
            {
                adornerManager.Remove();
            }
            e.Handled = true;
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            if (adornerManager != null)
            {
                var inputElement = sender as IInputElement;
                if (inputElement != null)
                {
                    var pt = e.GetPosition(inputElement);

                    var element = sender as UIElement;
                    if (element != null)
                    {
                        if (!pt.Within(element.RenderSize))
                        {
                            adornerManager.Remove();
                        }
                    }
                }
            }
            e.Handled = true;
        }

        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (adornerManager != null)
            {
                var element = sender as UIElement;
                if (element != null)
                {
                    adornerManager.Update(element);
                }
            }
            e.Handled = true;
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            if (adornerManager == null)
            {
                var element = sender as UIElement;
                if (element != null)
                {
                    adornerManager = new AdornerManager(
                        AdornerLayer.GetAdornerLayer(element),
                        adornedElement => new UIElementDropAdorner(adornedElement));
                }
            }
            e.Handled = true;
        }
    }
}
