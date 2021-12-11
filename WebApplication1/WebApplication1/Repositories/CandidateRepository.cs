using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext appDbContext;
        public CandidateRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Candidate> AddCandidate(Candidate candidate)
        {
            var result = await appDbContext.Candidates.AddAsync(candidate);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteCandidate(int candidateId)
        {
            var result = await appDbContext.Candidates
                .FirstOrDefaultAsync(c => c.Id == candidateId);
            if (result != null)
            {
                appDbContext.Candidates.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Candidate> GetCandidate(int candidateId)
        {
            return await appDbContext.Candidates
                //.Include(c => c.Results)
                .FirstOrDefaultAsync(c => c.Id == candidateId);
        }

        public async Task<Candidate> GetCandidateByCode(string code)
        {
            return await appDbContext.Candidates
                //.Include(c => c.Results)
                .FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            return await appDbContext.Candidates.Include(c => c.Results).ToListAsync();
        }

        public async Task<Candidate> UpdateCandidate(Candidate candidate)
        {
            var result = await appDbContext.Candidates
                .FirstOrDefaultAsync(c => c.Id == candidate.Id);
            if (result != null)
            {
                result.FirstName = candidate.FirstName;
                result.LastName = candidate.LastName;
                result.Code = candidate.Code;
                
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
