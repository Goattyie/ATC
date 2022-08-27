using ATC.Wpf.Exporter.Interfaces;
using ATC.Wpf.Queries.Interfaces;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace ATC.Wpf.Exporter.QueryExporters
{
    internal class AvgCallTimeByCityQueryExporter : BaseExporter, IExporter<AvgCallTimeResult>
    {
        public async Task<Stream> Export(params AvgCallTimeResult[] data)
        {
            var table = new DataTable();

            table.Columns.Add(new DataColumn("Название", typeof(string)));
            table.Columns.Add(new DataColumn("Время", typeof(string)));

            foreach(var obj in data)
            {
                table.Rows.Add(obj.Name, obj.Avg.ToString());
            }

            return await ExportTable(table);
        }
    }
}
