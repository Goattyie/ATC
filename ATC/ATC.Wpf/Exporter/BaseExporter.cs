using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC.Wpf.Exporter
{
    internal class BaseExporter
    {
        public Task<Stream> ExportTable(DataTable table)
        {
            var stream = new MemoryStream();
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet();
            var headerRow = sheet.CreateRow(0);

            for (var i = 0; i < table.Columns.Count; i++)
            {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(table.Columns[i].Caption);
            }

            for (var i = 0; i < table.Rows.Count; i++)
            {
                var dataRow = sheet.CreateRow(i + 1);
                for (var j = 0; j < table.Columns.Count; j++)
                {
                    var cell = dataRow.CreateCell(j);
                    var value = table.Rows[i][j].ToString();

                    if (table.Columns[j].DataType == typeof(double))
                    {
                        cell.SetCellValue(double.Parse(value));
                    }
                    else
                    {
                        cell.SetCellValue(value);
                    }
                }
            }

            workbook.Write(stream, true);
            stream.Position = 0;

            return Task.FromResult((Stream)stream);
        }
    }
}
