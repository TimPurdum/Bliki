using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bliki.Data
{
    public class MarkdownEditorManager
    {
        public ToggleResult ToggleHeader(int level, string content, int start)
        {
            var lineTuple = GetLineAndIndex(content, start);
            var line = lineTuple.Item1;
            var lineIndex = lineTuple.Item2;
            var originalLength = line.Length;
            var originalLevel = 0;

            while (line.StartsWith("#"))
            {
                line = line.Remove(0, 1);
                originalLevel++;
            }
            
            line = line.TrimStart();

            if (originalLevel != level)
            {
                for (var i = 0; i < level; i++)
                {
                    line = line.Insert(0, "#");
                }
                if (level > 0)
                {
                    line = line.Insert(level, " ");
                }
            }
            

            content = content.Remove(lineIndex, originalLength);
            content = content.Insert(lineIndex, line);
            return new ToggleResult(content, level - originalLevel);
        }


        public ToggleResult ToggleMarker(TextMarker marker, string content, int start, int end)
        {
            var before = start > 0 ? content.Substring(0, start) : "";
            var after = end < content.Length ? content.Substring(end, content.Length - end) : "";
            var selected = start > 0 && end < content.Length ? content.Substring(start, end - start) : content;

            var regex = new Regex($@"(?:^|[^\*])({marker.RegexValue})(?:[^\*]|$)");
            var markersFound = 0;
            var offset = -1 * marker.Value.Length;
            // check outside
            if (regex.Matches(after).Count % 2 == 1)
            {
                markersFound++;
                var firstAfterPosition = after.IndexOf(marker.Value) + end;
                content = content.Remove(firstAfterPosition, 2);
            }
            if (regex.Matches(before).Count % 2 == 1)
            {
                markersFound++;
                var lastBeforePosition = before.LastIndexOf(marker.Value);
                content = content.Remove(lastBeforePosition, 2);
            }

            // check inside
            var insideMatches = regex.Matches(selected);
            if (insideMatches.Any())
            {
                markersFound++;
                var firstInsidePosition = selected.IndexOf(marker.Value) + start;
                var lastInsidePosition = selected.LastIndexOf(marker.Value) + start;
                content = content.Remove(lastInsidePosition, 2);
                if (lastInsidePosition != firstInsidePosition)
                {
                    content = content.Remove(firstInsidePosition, 2);
                }
            }

            if (markersFound == 0)
            {
                offset = marker.Value.Length;
                content = content.Insert(end, marker.Value);
                content = content.Insert(start, marker.Value);
            }

            return new ToggleResult(content, offset);
        }


        private Tuple<string, int> GetLineAndIndex(string content, int start)
        {
            var allLines = _lineBreakRegex.Split(content);
            var index = 0;
            foreach (var line in allLines)
            {
                var lineIndex = index;
                index += line.Length + 1;
                if (index >= start)
                {
                    return new Tuple<string, int>(line, lineIndex);
                }
            }
            return new Tuple<string, int>("", 0);
        }


        private readonly Regex _boldRegex = new Regex(@"(?:^|[^\*])(\*\*)(?:[^\*]|$)");
        private readonly Regex _italicRegex = new Regex(@"(?:^|[^\*])(\*)(?:[^\*]|$)");
        private readonly Regex _strikethroughRegex = new Regex(@"(?:^|[^~])(~~)(?:[^~]|$)");
        private readonly Regex _lineBreakRegex = new Regex("\r\n|\r|\n");
        private const string _boldMarker = "**";
    }
}
