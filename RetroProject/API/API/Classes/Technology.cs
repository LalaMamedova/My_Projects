namespace API.Classes
{
    public class Technology
    {
        public int id { get; set; }
        public string name { get; set; } 
        public string description { get; set; } 
        public int year { get; set; }
        public string type { get; set; }
        public List<string> images { get; set; }
        public List<string> charname { get; set; }
        public List<string> charvalue { get; set; }
        public List<string> interestingfacts { get; set; }
    }
}
