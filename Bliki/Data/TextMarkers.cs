namespace Bliki.Data
{
    public static class TextMarkers
    {
        public static TextMarker Bold = new TextMarker("**", @"\*\*");
        public static TextMarker Italic = new TextMarker("*", @"\*");
        public static TextMarker Strikethrough = new TextMarker("~~", "~~");
    }

    public class TextMarker
    {
        public TextMarker(string value, string regexValue)
        {
            Value = value;
            RegexValue = regexValue;
        }

        public string Value { get; }
        public string RegexValue { get; }
    }
}
