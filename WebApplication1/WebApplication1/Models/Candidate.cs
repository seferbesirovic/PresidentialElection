using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Candidate
    {
        public int Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Code { get; set; }
        public List<Result> Results { get; set; }       
    }
}
