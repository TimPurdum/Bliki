using System;

namespace Bliki.Data
{
    public static class TextMarkers
    {
        public static TextMarker Bold = new TextMarker("**", @"(?:^|[^\*])(\*\*)(?:[^\*]|$)");
        public static TextMarker Italic = new TextMarker("*", @"(?:^|[^\*])(\*)(?:[^\*]|$)");
        public static TextMarker Strikethrough = new TextMarker("~~", @"(?:^|[^~])(~~)(?:[^~]|$)");
        public static TextMarker InlineCode = new TextMarker("`", @"(?:^|[^`])(`)(?:[^`]|$)");
        public static TextMarker CodeBlock = new TextMarker($"```", @"(?:^|[^`])(```)(?:[^`]|$)", true);
    }

    public struct TextMarker
    {
        public TextMarker(string value, string regexValue, bool isBlock = false)
        {
            Value = value;
            RegexValue = regexValue;
            IsBlock = isBlock;
        }

        public string Value { get; }
        public string RegexValue { get; }
        public bool IsBlock { get; }
    }
}
