namespace Bliki.Data
{
    public class NavPageMeta
    {
        public NavPageMeta(string title, string pageLink)
        {
            Title = title;
            PageLink = pageLink;
        }

        public string Title { get; }
        public string PageLink { get; }
    }
}
