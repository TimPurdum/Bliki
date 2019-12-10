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

        public override bool Equals(object? obj)
        {
            return obj is NavPageMeta other && Equals(other);
        }

        public bool Equals(NavPageMeta other)
        {
            return other.PageLink == PageLink &&
                other.Title == Title;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
