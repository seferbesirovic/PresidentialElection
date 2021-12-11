export class GetFormattedResultDto
{
     Id: number;
     CandidateId: number;
     CandidateFirstName: string; 
     CandidateLastName: string;
     StateId: number;
     Votes: number;
     AllCandidatesVotes: number;
     ErrorFlag: boolean;
     PercentageOfVotes: number;
}