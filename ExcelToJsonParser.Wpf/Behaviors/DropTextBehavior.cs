﻿using Microsoft.Xaml.Behaviors;
using NPOI.HSSF.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ExcelToJsonParser.Wpf.Behaviors
{
    internal class DropTextBehavior : Behavior<UIElement>
    {
        private Point _startPoint;

        protected override void OnAttached()
        {
            AssociatedObject.MouseDown += OnButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseDown -= OnButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
        }



        private void OnButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseUp += OnMouseUp;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
            AssociatedObject.ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var obj = AssociatedObject;

            var currentPos = e.GetPosition(obj);
            var delta = currentPos - _startPoint;

            //obj.SetValue()
        }
    }
}