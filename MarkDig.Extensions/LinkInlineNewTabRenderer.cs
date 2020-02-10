using Markdig.Renderers;
using Markdig.Renderers.Html.Inlines;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarkDig.Extensions
{
    public class LinkInlineNewTabRenderer: LinkInlineRenderer
    {
        protected override void Write(HtmlRenderer renderer, LinkInline link)
        {
            if (renderer.EnableHtmlForInline)
            {
                renderer.Write(link.IsImage ? "<img src=\"" : "<a href=\"");
                renderer.WriteEscapeUrl(link.GetDynamicUrl != null ? link.GetDynamicUrl() ?? link.Url : link.Url);
                renderer.Write("\"");
                renderer.WriteAttributes(link);
            }
            if (link.IsImage)
            {
                if (renderer.EnableHtmlForInline)
                {
                    renderer.Write(" alt=\"");
                }
                var wasEnableHtmlForInline = renderer.EnableHtmlForInline;
                renderer.EnableHtmlForInline = false;
                renderer.WriteChildren(link);
                renderer.EnableHtmlForInline = wasEnableHtmlForInline;
                if (renderer.EnableHtmlForInline)
                {
                    renderer.Write("\"");
                }
            }

            if (renderer.EnableHtmlForInline && !string.IsNullOrEmpty(link.Title))
            {
                renderer.Write(" title=\"");
                renderer.WriteEscape(link.Title);
                renderer.Write("\"");
            }

            if (!string.IsNullOrEmpty(Path.GetExtension(link.Url)))
            {
                renderer.Write(" target=\"_blank\"");
            }

            if (link.IsImage)
            {
                if (renderer.EnableHtmlForInline)
                {
                    renderer.Write(" />");
                }
            }
            else
            {
                if (renderer.EnableHtmlForInline)
                {
                    if (AutoRelNoFollow)
                    {
                        renderer.Write(" rel=\"nofollow\"");
                    }
                    renderer.Write(">");
                }
                renderer.WriteChildren(link);
                if (renderer.EnableHtmlForInline)
                {
                    renderer.Write("</a>");
                }
            }
        }
    }
}
