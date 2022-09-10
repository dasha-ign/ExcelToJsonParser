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
                    _DragItemViewSource = new()
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
            StudentsView?.Refresh();
            DragItemsView?.Refresh();
        }

        #endregion

        #endregion


    }
}
