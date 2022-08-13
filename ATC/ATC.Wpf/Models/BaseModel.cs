using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal abstract class BaseModel
    {
        [DisplayName("id")]
        public virtual int Id { get; set; }
    }
}
