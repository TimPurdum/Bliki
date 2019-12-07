namespace Bliki.Data
{
    public class WikiPageModel
    {
        public string? Title { get; set; }
        public string PageLink { get; set; } = "new-page";
        public string Content { get; set; } = "";
        public string SubTitle { get; set; } = "";
    }
}
