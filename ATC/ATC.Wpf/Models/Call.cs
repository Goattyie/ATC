using System;
using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class CallModel : BaseModel
    {
        [DisplayName("atc_id")]
        public int AtcId { get; set; }
        
        [DisplayName("atc_name")]
        public string AtcName { get; set; }
        
        [DisplayName("city_id")]
        public int CityId { get; set; }
        
        [DisplayName("city_name")]
        public string CityName { get; set; }
        
        [DisplayName("cost")]
        public decimal Cost { get; set; }
        
        [DisplayName("phone")]
        public string Phone { get; set; }
        
        [DisplayName("call_date")]
        public DateTime CallDate { get; set; }
        
        [DisplayName("time")]
        public TimeSpan Time  { get; set; }
        
        [DisplayName("tariff_id")]
        public int TariffId { get; set; }
        
        [DisplayName("tariff_name")]
        public string TariffName { get; set; }
        
        [DisplayName("abonent_id")]
        public int AbonentId { get; set; }
        
        [DisplayName("abonent_phone")]
        public string AbonentPhone { get; set; }
    }
}
