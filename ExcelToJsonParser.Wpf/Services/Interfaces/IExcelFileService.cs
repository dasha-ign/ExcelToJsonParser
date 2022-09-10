using ExcelToJsonParser.Wpf.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ExcelToJsonParser.Wpf.Services.Interfaces
{
    internal interface IExcelFileService
    {
        Task<ObservableCollection<DragItem>> CreateDragItems();
        Task<ObservableCollection<Student>> CreateStudents();
    }
}