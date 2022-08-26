using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class AreasWithCitiesAndCountriesResult
    {
        [DisplayName("id")]
        public int Id { get; set; }
        
        [DisplayName("name")]
        public string Name { get; set; }
    }

    internal interface IAreasWithCitiesAndCountriesQuery : IQuery<BaseInput, AreasWithCitiesAndCountriesResult>
    {
    }
}
