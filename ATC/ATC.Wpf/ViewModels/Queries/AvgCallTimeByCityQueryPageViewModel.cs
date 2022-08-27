using ATC.Wpf.Exporter.Interfaces;
using ATC.Wpf.Queries.Interfaces;
using DevExpress.Mvvm;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AvgCallTimeByCityQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, AvgCallTimeResult>
    {
        private readonly IExporter<AvgCallTimeResult> _exporter;

        public AvgCallTimeByCityQueryPageViewModel(IAvgCallTimeByCityQuery query, IExporter<AvgCallTimeResult> exporter) : base(query)
        {
            _exporter = exporter;

            SeriesCollection = new();
            Labels = new();
        }
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> Formatter { get; set; } = value => Math.Round(value, 2).ToString();

        public override IAsyncCommand ExecuteCommand => new AsyncCommand(async () =>
        {
            Data.Clear();
            SeriesCollection.Clear();
            Labels.Clear();

            var result = await Query.ExecuteQuery(InputData);

            foreach (var item in result)
                Data.Add(item);

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Series A",
                Values = new ChartValues<double>(Data.Select(x => x.Avg.TotalMinutes)),
                Fill = Brushes.DarkRed
            });

            Labels.AddRange(Data.Select(x => x.Name));
        });

        public IAsyncCommand ExportCommand => new AsyncCommand(async () =>
        {
            var stream = await _exporter.Export(Data.ToArray());

            using var filestream = new FileStream($"{string.Format("{0:ddMMyyyyHHmm}", DateTime.Now)}.xlsx", FileMode.Create, FileAccess.Write);

            await stream.CopyToAsync(filestream);
            stream.Close();
        });
    }
}
