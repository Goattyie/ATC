using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class SocialStatus : BaseModel
    {
        [DisplayName("name")]
        public string Name { get; set; }
    }
}
