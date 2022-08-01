namespace ATC.Wpf.Models
{
    internal class City : BaseModel
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
