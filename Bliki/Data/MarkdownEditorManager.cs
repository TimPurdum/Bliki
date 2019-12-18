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
            var selected = start >= 0 && end <= content.Length ? content.Substring(start, end - start) : content;
            if (!marker.IsBlock)
            {
                var lineTuple = GetLineAndIndex(content, start);
                before = before.Substring(lineTuple.Item2);
                after = after.Substring(0, (lineTuple.Item2 + lineTuple.Item1.Length) - start);
            }

            var regex = new Regex($@"(?:^|[^\*])({marker.RegexValue})(?:[^\*]|$)");
            var markersFound = 0;
            var offset = -1 * marker.Value.Length;
            // check outside
            if (regex.Matches(after).Count % 2 == 1)
            {
                markersFound++;
                // Special case: if the cursor is right before the final mark, jump outside.
                if (start == end && after.IndexOf(marker.Value) == 0 &&
                    before.LastIndexOf(marker.Value) < start - 2)
                {
                    return new ToggleResult(content, marker.Value.Length);
                }

                var firstAfterPosition = after.IndexOf(marker.Value) + end;
                content = content.Remove(firstAfterPosition, 2);
            }
            if (regex.Matches(before).Count % 2 == 1)
            {
                markersFound++;
                var lastBeforePosition = content.Substring(0, start).LastIndexOf(marker.Value);
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
                offset = marker.Value.Length + (marker.IsBlock ? 1 : 0);
                content = content.Insert(end, marker.Value + (marker.IsBlock ? Environment.NewLine : ""));
                content = content.Insert(start, (marker.IsBlock ? Environment.NewLine : "") + marker.Value);
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
                if (index > start)
                {
                    return new Tuple<string, int>(line, lineIndex);
                }
            }
            return new Tuple<string, int>("", 0);
        }


        private readonly Regex _lineBreakRegex = new Regex("\r\n|\r|\n");


        public ToggleResult ToggleNumberedList(string content, int start)
        {
            var (line, lineIndex) = GetLineAndIndex(content, start);
            var originalIndent = 0;

            while (line.StartsWith(" "))
            {
                line = line.Remove(0, 1);
                originalIndent++;
            }
            
            var intRgx = new Regex(@"^([\d]+\. )");
            var match = intRgx.Match(line);
            var offset = 0;
            if (match.Success)
            {
                // This was a numbered line, remove the number
                content = content.Remove(lineIndex + originalIndent, match.Length);
                offset = -1 * match.Length;
            }
            else
            {
                // add a number
                var previousLineTuple = GetLineAndIndex(content, lineIndex - 1);
                var previousLine = previousLineTuple.Item1.Trim();
                var prevLineMatch = intRgx.Match(previousLine);
                var newNum = 1;
                if (prevLineMatch.Success)
                {
                    newNum = int.Parse(prevLineMatch.Value.TrimEnd().TrimEnd('.')) + 1;
                }

                var insert = $"{newNum}. ";
                content = content.Insert(lineIndex + originalIndent, insert);
                offset = insert.Length;
            }
            
            return new ToggleResult(content, offset);
        }

        public ToggleResult InsertTab(string content, int start, int end)
        {
            var selected = start >= 0 && end <= content.Length ? content.Substring(start, end - start) : content;
            content = content.Remove(start, selected.Length);
            content = content.Insert(start, "    ");

            return new ToggleResult(content, 4 - selected.Length);
        }

        public ToggleResult ToggleBulletList(string content, int start)
        {
            var (line, lineIndex) = GetLineAndIndex(content, start);
            var originalIndent = 0;

            while (line.StartsWith(" "))
            {
                line = line.Remove(0, 1);
                originalIndent++;
            }
            
            var offset = 0;
            if (line.StartsWith("- "))
            {
                // This was a bulleted line, remove the number
                content = content.Remove(lineIndex + originalIndent, 2);
                offset = -2;
            }
            else
            {
                // add a bullet
                content = content.Insert(lineIndex + originalIndent, "- ");
                offset = 2;
            }
            
            return new ToggleResult(content, offset);
        }
    }
}
