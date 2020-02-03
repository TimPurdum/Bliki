using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;


namespace MarkDig.Extensions
{
    public class CodeBlockHighlighting : IMarkdownExtension
    {
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
        }


        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            renderer.ObjectRenderers.ReplaceOrAdd<CodeBlockRenderer>(
                new CodeBlockHighlightingRenderer());
        }
    }


    public class CodeBlockHighlightingRenderer : CodeBlockRenderer
    {
        protected override void Write(HtmlRenderer renderer, CodeBlock obj)
        {
            var attrs = obj.GetAttributes();
            attrs.AddProperty("background", "#f6f8f");
            obj.SetAttributes(attrs);
            base.Write(renderer, obj);
        }
    }
}