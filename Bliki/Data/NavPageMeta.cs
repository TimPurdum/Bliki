namespace Bliki.Data
{
    public class NavPageMeta
    {
        public NavPageMeta(string title, string pageLink, string? folder)
        {
            Title = title;
            PageLink = pageLink;
            Folder = folder;
        }


        public string Title { get; }
        public string PageLink { get; }
        public string? Folder { get; }


        public override bool Equals(object? obj)
        {
            return obj is NavPageMeta other && Equals(other);
        }


        public bool Equals(NavPageMeta other)
        {
            return other.PageLink == PageLink &&
                   other.Title == Title &&
                   other.Folder == Folder;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}