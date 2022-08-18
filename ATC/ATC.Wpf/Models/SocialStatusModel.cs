using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class SocialStatusModel : BaseModel
    {
        [DisplayName("name")]
        public string Name { get; set; }
    }
}
