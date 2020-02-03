namespace Bliki.Data
{
    public class ToggleResult
    {
        public ToggleResult(string content, int offset)
        {
            Content = content;
            Offset = offset;
        }


        public string Content { get; set; }
        public int Offset { get; set; }
    }
}