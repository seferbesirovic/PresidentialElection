namespace WebApplication1.Models
{
    public class Result
    {
        //
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public int StateId  { get; set; }
        public State State { get; set; }
        public int Votes { get; set; }
        public bool ErrorFlag { get; set; }

    }
}
