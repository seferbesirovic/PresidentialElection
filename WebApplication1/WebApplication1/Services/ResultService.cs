using System;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository resultRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IStateRepository stateRepository;
        public ResultService(IResultRepository resultRepository, ICandidateRepository candidateRepository, IStateRepository stateRepository)
        {
            this.resultRepository = resultRepository;
            this.candidateRepository = candidateRepository;
            this.stateRepository = stateRepository;
        }
        public async Task<Result> CreateResult(string candidateCode, string stateName, int votes)
        {
            var candidate = await candidateRepository.GetCandidateByCode(candidateCode);
            if (candidate == null)
            {
                throw new Exception("Candidate code does not exist");
            }

            var state = await stateRepository.GetStateByName(stateName);
            if (state == null)
            {
                throw new Exception("State name does not exist");
            }

            var existingResult = await resultRepository.GetResultsByCandidateIdAndStateId(candidate.Id, state.Id);
            if (existingResult != null)
            {
                throw new Exception("Result with this candidateId and stateId already exist");
            }

            Result res = new Result
            {
                CandidateId = candidate.Id,
                Candidate = candidate,
                State = state,
                StateId = state.Id,
                Votes = votes
            };

            return await resultRepository.AddResult(res);
        }

        public async Task<Result> UpdateResult(string candidateCode, string stateName, int votes)
        {
            var candidate = await candidateRepository.GetCandidateByCode(candidateCode);
            if (candidate == null)
            {
                throw new Exception("Candidate code does not exist");
            }

            var state = await stateRepository.GetStateByName(stateName);
            if (state == null)
            {
                throw new Exception("State name does not exist");
            }

            var existingResult = await resultRepository.GetResultsByCandidateIdAndStateId(candidate.Id, state.Id);
            if (existingResult == null)
            {
                throw new Exception("Result with this candidateId and stateId doesn't exist");
            }
            existingResult.Votes = votes;

            return await resultRepository.UpdateResult(existingResult);
        }

        public async Task<Result> CreateResult(string candidateCode, string stateName, int votes, bool success)
        {
            var candidate = await candidateRepository.GetCandidateByCode(candidateCode);
            if (candidate == null)
            {
                //throw new Exception("Candidate code does not exist");
                return null;
            }

            var state = await stateRepository.GetStateByName(stateName);
            if (state == null)
            {
                //throw new Exception("State name does not exist");
                return null;
            }
            if (!success || votes < 0)
            {
                votes = 0;
                success = false;
            } // notRedundant, service validate itself
            
            var existingResult = await resultRepository.GetResultsByCandidateIdAndStateId(candidate.Id, state.Id);
            if (existingResult == null)
            {
                Result res = new Result
                {
                    CandidateId = candidate.Id,
                    Candidate = candidate,
                    State = state,
                    StateId = state.Id,
                    Votes = votes,
                    ErrorFlag = !success
                };

                return await resultRepository.AddResult(res);
            }
            return null;
        }

        public async Task<Result> UpdateResult(string candidateCode, string stateName, int votes, bool success)
        {
            var candidate = await candidateRepository.GetCandidateByCode(candidateCode);
            var state = await stateRepository.GetStateByName(stateName);

            if (candidate == null || state == null || !success || votes < 0)
                return null;

            var existingResult = await resultRepository.GetResultsByCandidateIdAndStateId(candidate.Id, state.Id);
            if (existingResult != null)
            {
                existingResult.Votes = votes;
                existingResult.ErrorFlag = !success;
                return await resultRepository.UpdateResult(existingResult);
            }
            return null;
        }
    }
}
