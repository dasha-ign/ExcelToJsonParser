using ExcelToJsonParser.Wpf.Models;
using ExcelToJsonParser.Wpf.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToJsonParser.Wpf.Services
{
    internal class ExcelFileService : IExcelFileService
    {
        public async Task<ObservableCollection<Student>> CreateStudents()
        {
            ObservableCollection<Student> students = new();
            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                students.Add(new Student()
                {
                    FirstName = $"Имя_{i}",
                    LastName = $"Фамилия_{i}",
                    BirthDate = new(random.Next(1955, 2005),
                                    random.Next(1, 12),
                                    random.Next(1, 28)),
                    Group = $"Группа{i % 10}"
                });
            }

            return students;
        }


        public async Task<ObservableCollection<DragItem>> CreateDragItems()
        {
            ObservableCollection<DragItem> dragItems = new();
            dragItems.Add(new("отличник"));
            dragItems.Add(new("ударник"));
            dragItems.Add(new("двоечник"));
            return dragItems;
        }
    }
}
