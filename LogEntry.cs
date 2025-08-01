namespace TrailAndTailLogBook.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Location { get; set; }
        public byte[] Photo { get; set; }
    }
}