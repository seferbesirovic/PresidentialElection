namespace WebApplication1.Dtos
{
    public class GetFormattedResultDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string CandidateFirstName { get; set; } 
        public string CandidateLastName { get; set; }
        public int StateId { get; set; }
        public int Votes { get; set; }
        public int AllCandidatesVotes { get; set; }
        public bool ErrorFlag { get; set; }

        public int PercentageOfVotes { get; set; }
    }
}
