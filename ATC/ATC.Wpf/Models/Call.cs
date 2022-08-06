using System;

namespace ATC.Wpf.Models
{
    internal class Call : BaseModel
    {
        public int AtcId { get; set; }
        public Atc Atc { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public decimal Cost { get; set; }
        public string Phone { get; set; }
        public DateTime CallDate { get; set; }
        public TimeSpan Time  { get; set; }
        public int TariffId { get; set; }
        public Tariff Tariff { get; set; }
        public int AbonentId { get; set; }
        public Abonent Abonent { get; set; }
    }
}
