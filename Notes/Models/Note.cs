namespace Notes.Models
{
    [Serializable]
    public class Note
    {
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime CreateDT { get; set; }
        public string[] Tags { get; set; } = null!;

        public string GetTagsString(string sep = "/")
        {
            return String.Join(sep, Tags);
        }
    }
}
