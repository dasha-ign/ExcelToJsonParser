using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToJsonParser.Wpf.Models
{
    internal class Student
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Group { get; set; }

        public override string ToString()
        {
            return $"{Group}: {LastName} {FirstName}({BirthDate}";
        }
    }
}
