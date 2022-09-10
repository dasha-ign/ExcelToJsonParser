using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToJsonParser.Wpf.Models
{
    internal class DragItem
    {
        public string? Caption { get; set; }

        public DragItem(string caption)
        {
            Caption = caption;
        }

        public override string ToString()
        {
            return Caption;
        }
    }
}
