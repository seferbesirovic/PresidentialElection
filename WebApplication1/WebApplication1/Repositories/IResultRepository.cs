using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IResultRepository
    {
        Task<IEnumerable<Result>> GetResults();
        Task<Result> GetResult(int id);
        Task<IEnumerable<Result>> GetResultsByName(string name);
        Task<Result> AddResult(Result result);
        Task<Result> UpdateResult(Result result);
        Task DeleteResult(int resultId);
        Task<Result> GetResultsByCandidateIdAndStateId(int candidateId, int stateId);
    }
}
