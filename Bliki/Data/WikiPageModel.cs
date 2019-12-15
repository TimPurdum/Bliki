namespace Bliki.Data
{
    public class WikiPageModel
    {
        public string? Title { get; set; }
        public string PageLink { get; set; } = "new-page";
        public string Content { get; set; } = "";
        public string SubTitle { get; set; } = "";

        public override bool Equals(object? obj)
        {
            return obj is WikiPageModel other && Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(WikiPageModel other)
        {
            return other.Title == Title &&
                other.Content == Content &&
                other.PageLink == PageLink;
        }
    }
}
