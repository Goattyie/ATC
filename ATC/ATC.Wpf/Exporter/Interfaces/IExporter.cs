using System.IO;
using System.Threading.Tasks;

namespace ATC.Wpf.Exporter.Interfaces
{
    internal interface IExporter<T>
    {
        Task<Stream> Export(params T[] data);
    }
}
