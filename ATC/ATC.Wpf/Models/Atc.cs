namespace ATC.Wpf.Models
{
    internal class Atc : BaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public string Address { get; set; }
        public int OpenYear { get; set; }
        public bool IsState { get; set; }
        public bool License { get; set; }
    }
}
