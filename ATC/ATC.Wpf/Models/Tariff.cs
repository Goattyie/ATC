using System;

namespace ATC.Wpf.Models
{
    internal class Tariff : BaseModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Ratio { get; set; }
    }
}
