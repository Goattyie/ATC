using System;
using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class Tariff : BaseModel
    {
        [DisplayName("name")]
        public string Name { get; set; }
        
        [DisplayName("start_date")]
        public DateTime StartDate { get; set; }
        
        [DisplayName("end_date")]
        public DateTime EndDate { get; set; }
        
        [DisplayName("ratio")]
        public double Ratio { get; set; }
    }
}
