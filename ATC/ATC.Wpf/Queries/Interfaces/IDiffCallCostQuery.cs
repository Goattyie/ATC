using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class DiffCallCostResult
    {
        [DisplayName("first_name")]
        public string FirstName { get; set; }
        
        [DisplayName("second_name")]
        public string SecondName { get; set; }
        
        [DisplayName("last_name")]
        public string LastName { get; set; }
        
        [DisplayName("phone")]
        public string Phone { get; set; }
        
        [DisplayName("diff")]
        public decimal Diff { get; set; }
    }

    internal interface IDiffCallCostQuery : IQuery<BaseInput, DiffCallCostResult>
    {
    }
}
