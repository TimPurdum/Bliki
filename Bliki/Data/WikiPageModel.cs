using System.ComponentModel;

namespace Bliki.Data
{
    public class WikiPageModel : INotifyPropertyChanged
    {
        private string content = "";

        public string? Title { get; set; }
        public string PageLink { get; set; } = "new-page";
        public string Content {
            get => content; set {
                content = value;
                PropertyChanged?
                    .Invoke(this, new PropertyChangedEventArgs("Content"));
            }
        }
        public string SubTitle { get; set; } = "";

        public event PropertyChangedEventHandler? PropertyChanged;

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
