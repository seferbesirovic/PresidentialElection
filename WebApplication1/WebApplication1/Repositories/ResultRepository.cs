using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly AppDbContext appDbContext;
        public ResultRepository( AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Result> AddResult(Result result)
        {
            var res = await appDbContext.Results.AddAsync(result);
            await appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task DeleteResult(int resultId)
        {
            var result = await appDbContext.Results
                .FirstOrDefaultAsync(r => r.Id == resultId);
            if (result != null)
            {
                appDbContext.Results.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Result> GetResult(int id)
        {
            return await appDbContext.Results
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Result>> GetResultsByName(string name)
        {
            return await appDbContext.Results
                .Include(r => r.State)
                .Where(r => r.State.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Result>> GetResults()
        {
            return await appDbContext.Results.Include(r => r.Candidate).ToListAsync();
        }

        public async Task<Result> UpdateResult(Result result)
        {
            var res = await appDbContext.Results
                .FirstOrDefaultAsync(r => r.Id == result.Id);
            if (result != null)
            {
                res.Votes = result.Votes;
                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }

        public async Task<Result> GetResultsByCandidateIdAndStateId(int candidateId, int stateId)
        {
            return await appDbContext.Results
                .Include(r => r.State)
                .Include(r => r.Candidate)
                .FirstOrDefaultAsync(r => r.State.Id == stateId && r.Candidate.Id == candidateId);
        }
    }
}
