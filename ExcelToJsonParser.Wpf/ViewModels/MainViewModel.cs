using ExcelToJsonParser.Wpf.Models;
using ExcelToJsonParser.Wpf.Services.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ExcelToJsonParser.Wpf.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private readonly IExcelFileService _excelFileService;

        public MainViewModel(IExcelFileService excelFileService)
        {
            _excelFileService = excelFileService;
        }


        #region view model properties


        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        public string Title => "Drag and drop custom items on DataGrid";

        #endregion

        #region IsLoading : bool - if data is loading

        ///<summary>if data is loading</summary>
        public bool IsLoading =>  false;

        #endregion


        #region DragItems : ObservableCollection<DragItem> - items to drag onto and from datagrid

        ///<summary>items to drag onto and from datagrid</summary>
        private ObservableCollection<DragItem> _DragItems;

        ///<summary>items to drag onto and from datagrid</summary>
        public ObservableCollection<DragItem> DragItems
        {
            get => _DragItems;
            set
            {
                if (Set(ref _DragItems, value))
                {
                    _DragItemViewSource = new CollectionViewSource()
                    {
                        Source = value,
                        SortDescriptions =
                        {
                            new SortDescription(nameof(DragItem.Caption), ListSortDirection.Ascending)
                        }
                    };
                    _DragItemViewSource.View.Refresh();
                }
            }
        }

        private CollectionViewSource _DragItemViewSource;
        public ICollectionView? DragItemsView => _DragItemViewSource?.View;

        #endregion


        #region SelectedDragItem : DragItem - selected item to drag somewhere

        ///<summary>selected item to drag somewhere</summary>
        private DragItem _SelectedDragItem;

        ///<summary>selected item to drag somewhere</summary>
        public DragItem SelectedDragItem { get => _SelectedDragItem; set => Set(ref _SelectedDragItem, value); }

        #endregion


        #region Students : ObservableCollection<Student> - list of students

        ///<summary>list of students</summary>
        private ObservableCollection<Student> _Students;

        ///<summary>list of students</summary>
        public ObservableCollection<Student> Students
        {
            get => _Students;
            set
            {
                if (Set(ref _Students, value))
                {
                    _StudentsViewSource = new()
                    {
                        Source = value,
                        SortDescriptions =
                        {
                            new SortDescription(nameof(Student.Group), ListSortDirection.Ascending),
                            new SortDescription(nameof(Student.LastName), ListSortDirection.Ascending)
                        }
                    };
                    _StudentsViewSource.View.Refresh();
                }
            }
        }

        private CollectionViewSource _StudentsViewSource;
        public ICollectionView? StudentsView => _StudentsViewSource?.View;

        #endregion


        #region SelectedStudent : Student - selected student

        ///<summary>selected student</summary>
        private Student _SelectedStudent;

        ///<summary>selected student</summary>
        public Student SelectedStudent { get => _SelectedStudent; set => Set(ref _SelectedStudent, value); }

        #endregion



        #region ColumnIndex : int? - index of the column mouse is over

        ///<summary>index of the column mouse is over</summary>
        private int? _ColumnIndex;

        ///<summary>index of the column mouse is over</summary>
        public int? ColumnIndex { get => _ColumnIndex; set => Set(ref _ColumnIndex, value); }

        #endregion


        #endregion

        #region view model commands

        #region Command _LoadDataCommand - fills list of students and drag items

        /// <summary>fills list of students and drag items</summary>
        private ICommand _LoadDataCommand;

        /// <summary>fills list of students and drag items</summary>
        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

        /// <summary>Проверка возможности выполнения - fills list of students and drag items</summary>
        private bool CanLoadDataCommandExecute() => true;

        /// <summary>Логика выполнения - fills list of students and drag items</summary>
        private async Task OnLoadDataCommandExecuted()
        {
            Students = await _excelFileService.CreateStudents();
            DragItems = await _excelFileService.CreateDragItems();
            OnPropertyChanged(nameof(DragItems));
            StudentsView?.Refresh();
            DragItemsView?.Refresh();
        }

        #endregion

        #region Command _RenameColumnCommand - renames column header at drop

        /// <summary>renames column header at drop</summary>
        private ICommand _RenameColumnCommand;

        /// <summary>renames column header at drop</summary>
        public ICommand RenameColumnCommand => _RenameColumnCommand
            ??= new LambdaCommandAsync(OnRenameColumnCommandExecuted, CanRenameColumnCommandExecute);

        /// <summary>Проверка возможности выполнения - renames column header at drop</summary>
        private bool CanRenameColumnCommandExecute() => true;

        /// <summary>Логика выполнения - renames column header at drop</summary>
        private async Task OnRenameColumnCommandExecuted()
        {

        }

        #endregion

        #region Command _ShowMessageCommand - displays dropped data

        /// <summary>displays dropped data</summary>
        private ICommand _ShowMessageCommand;

        /// <summary>displays dropped data</summary>
        public ICommand ShowMessageCommand => _ShowMessageCommand
            ??= new LambdaCommandAsync<string[]>(OnShowMessageCommandExecuted, CanShowMessageCommandExecute);

        /// <summary>Проверка возможности выполнения - displays dropped data</summary>
        private bool CanShowMessageCommandExecute(string[] message) => true;

        /// <summary>Логика выполнения - displays dropped data</summary>
        private async Task OnShowMessageCommandExecuted(string[] message)
        {
            
            MessageBox.Show($"{message[0]}");
            MessageBox.Show($"Column: {ColumnIndex}");
        }

        #endregion

        #endregion


    }
}
