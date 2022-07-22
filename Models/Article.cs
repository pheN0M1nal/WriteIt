namespace WriteIt.Models
{
    public class Article
    {
        public string? id { get; set; }
        public string writername { get; set; }
        public string title { get; set; }

        public string body { get; set; }
        public string timeofsubmission { get; set; }
        public string category { get; set; }



        public string img { get; set; }
        public string timeofreading { get; set; }
    }

}