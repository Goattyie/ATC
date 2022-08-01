namespace ATC.Wpf.Models
{
    internal class Area : BaseModel
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
