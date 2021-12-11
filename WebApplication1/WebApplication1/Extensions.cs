using WebApplication1.Models;
using WebApplication1.Dtos;
using System.Linq;

namespace WebApplication1
{
    public static class Extensions
    {
        public static GetCandidateDto AsDto(this Candidate candidate)
        {
            return new GetCandidateDto
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Code = candidate.Code,
            };
        }

        public static GetResultDto AsDto(this Result result)
        {
            return new GetResultDto
            {
                Id = result.Id,
                CandidateId = result.CandidateId,
                StateId = result.StateId,
                Votes = result.Votes,
            };
        }
        public static GetFormattedResultDto AsFormattedDto(this Result result)
        {

            int sumOfAllVotesFromCandidate = result.Candidate.Results.Sum(s => {
                if (s.CandidateId == result.CandidateId && !s.ErrorFlag)
                    return s.Votes;
                else return 0;
            });
            int percentageOfVotes = 0;
            if (sumOfAllVotesFromCandidate > 0)
            {
                percentageOfVotes = result.Votes * 100 / sumOfAllVotesFromCandidate;
            }
            return new GetFormattedResultDto
            {
                Id = result.Id,
                CandidateId = result.CandidateId,
                CandidateFirstName = result.Candidate.FirstName,
                CandidateLastName = result.Candidate.LastName,
                AllCandidatesVotes = sumOfAllVotesFromCandidate,
                StateId = result.StateId,
                Votes = result.Votes,
                ErrorFlag = result.ErrorFlag,
                PercentageOfVotes = percentageOfVotes
            };
        }
        public static GetStateDto AsDto(this State state)
        {
            return new GetStateDto
            {
                Id = state.Id,
                Name = state.Name,
            };
        }

        public static GetStateWithResultsDto AsFormattedDto(this State state)
        {
            return new GetStateWithResultsDto
            {
                Id = state.Id,
                Name = state.Name,
                Results = state.Results.Select(res => res.AsFormattedDto()).ToList()
            };
        }
    }
}
