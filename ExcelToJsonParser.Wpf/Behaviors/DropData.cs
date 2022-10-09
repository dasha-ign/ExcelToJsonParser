using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

using Microsoft.Xaml.Behaviors;
using System.ComponentModel;

namespace ExcelToJsonParser.Wpf.Behaviors;

public class DropData : Behavior<UIElement>
{
    #region DropDataCommand : ICommand - Команда, вызываемая в момент получения данных

    /// <summary>Команда, вызываемая в момент получения данных</summary>

    public static readonly DependencyProperty DropDataCommandProperty =
        DependencyProperty.Register(
            nameof(DropDataCommand),
            typeof(ICommand),
            typeof(DropData),
            new PropertyMetadata(default(ICommand)));

    /// <summary>Команда, вызываемая в момент получения данных</summary>
    public ICommand DropDataCommand
    {
        get => (ICommand)GetValue(DropDataCommandProperty);
        set => SetValue(DropDataCommandProperty, value);
    }

    #endregion

    #region DataFormat : string - Предпочитаемый формат данных

    /// <summary>Предпочитаемый формат данных</summary>
    public static readonly DependencyProperty DataFormatProperty =
        DependencyProperty.Register(
            nameof(DataFormat),
            typeof(string),
            typeof(DropData),
            new PropertyMetadata(default(string)));

    /// <summary>Предпочитаемый формат данных</summary>
    public string DataFormat
    {
        get => (string)GetValue(DataFormatProperty);
        set => SetValue(DataFormatProperty, value);
    }

    #endregion

    #region DataFormatAutoConvertation : bool - Автоматически преобразовывать данные

    /// <summary>Автоматически преобразовывать данные</summary>
    public static readonly DependencyProperty DataFormatAutoConvertationProperty =
        DependencyProperty.Register(
            nameof(DataFormatAutoConvertation),
            typeof(bool),
            typeof(DropData),
            new PropertyMetadata(true));

    /// <summary>Автоматически преобразовывать данные</summary>
    public bool DataFormatAutoConvertation
    {
        get => (bool)GetValue(DataFormatAutoConvertationProperty);
        set => SetValue(DataFormatAutoConvertationProperty, value);
    }

    #endregion

    #region DataType : Type - Предпочитаемый тип данных

    /// <summary>Предпочитаемый тип данных</summary>

    public static readonly DependencyProperty DataTypeProperty =
        DependencyProperty.Register(
            nameof(DataType),
            typeof(Type),
            typeof(DropData),
            new PropertyMetadata(default(Type)));

    /// <summary>Предпочитаемый тип данных</summary>
    public Type DataType
    {
        get => (Type)GetValue(DataTypeProperty);
        set => SetValue(DataTypeProperty, value);
    }

    #endregion

    #region Cursor : Cursor - Курсор

    /// <summary>Курсор</summary>
    public static readonly DependencyProperty CursorProperty =
        DependencyProperty.Register(
            nameof(Cursor),
            typeof(Cursor),
            typeof(DropData),
            new PropertyMetadata(default(Cursor)));

    /// <summary>Курсор</summary>
    //[Category("")]
    [Description("Курсор")]
    public Cursor Cursor { get => (Cursor)GetValue(CursorProperty); set => SetValue(CursorProperty, value); }

    #endregion


    private bool _LastAllowDropValue;
    protected override void OnAttached()
    {
        var element = AssociatedObject;
        _LastAllowDropValue = element.AllowDrop;
        element.AllowDrop = true;
        element.Drop += OnDropData;
        if (Cursor != null && element is FrameworkElement input)
        {
            input.DragEnter += OnElementDragEnter;
            input.DragLeave += OnElementDragLeave;
        }
    }

    protected override void OnDetaching()
    {
        var element = AssociatedObject;
        element.AllowDrop = _LastAllowDropValue;
        element.Drop -= OnDropData;
        if (Cursor != null && element is FrameworkElement input)
        {
            input.DragEnter -= OnElementDragEnter;
            input.DragLeave -= OnElementDragLeave;
        }
    }

    private Cursor _LastCursor;
    private void OnElementDragEnter(object Sender, DragEventArgs E)
    {
        if (!(Cursor is { } cursor) || !(AssociatedObject is FrameworkElement element)) return;
        _LastCursor = element.Cursor;
        element.Cursor = cursor;
    }

    private void OnElementDragLeave(object Sender, DragEventArgs E)
    {
        if (_LastCursor is null || !(AssociatedObject is FrameworkElement element)) return;
        element.Cursor = _LastCursor;
    }

    private void OnDropData(object Sender, DragEventArgs E)
    {
        var command = DropDataCommand;
        if (command is null) return;
        var data = E.Data;
        var data_type = DataType;
        if (data_type != null)
        {
            if (!data.GetDataPresent(data_type)) return;
            var value = data.GetData(data_type);
            if (command.CanExecute(value))
                command.Execute(value);
            return;
        }

        var data_format = DataFormat;
        if (!string.IsNullOrWhiteSpace(data_format))
        {
            var auto_convertation = DataFormatAutoConvertation;
            if (!data.GetDataPresent(data_format, auto_convertation)) return;
            var value = data.GetData(data_format, auto_convertation);
            if (command.CanExecute(value))
                command.Execute(value);
            return;
        }

        var str = data.GetData(DataFormats.StringFormat, true);
        if (command.CanExecute(str))
            command.Execute(str);
    }
}