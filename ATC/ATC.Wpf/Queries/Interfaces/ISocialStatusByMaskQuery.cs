namespace ATC.Wpf.Queries.Interfaces
{
    internal class SocialStatusByMaskResult : CountResult
    {
        [System.ComponentModel.DisplayName("name")]
        public string Name { get; set; }
    }

    internal interface ISocialStatusByMaskQuery : IQuery<StringInput, SocialStatusByMaskResult>
    {
    }
}
