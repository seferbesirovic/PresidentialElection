using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetCandidates();
        Task<Candidate> GetCandidate (int candidateId);
        Task<Candidate> GetCandidateByCode(string code);
        Task<Candidate> AddCandidate (Candidate candidate);
        Task<Candidate> UpdateCandidate (Candidate candidate);
        Task DeleteCandidate (int candidateId);
    }
}
