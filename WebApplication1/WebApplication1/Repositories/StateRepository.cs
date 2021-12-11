using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly AppDbContext appDbContext;
        public StateRepository(AppDbContext appDbContext)
        {
             this.appDbContext = appDbContext;
        }
        public async Task<State> AddState(State state)
        {
            if(state.Results != null)
            {
                appDbContext.Entry(state.Results).State = EntityState.Unchanged;
            }
            var result = await appDbContext.State.AddAsync(state);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteState(int stateId)
        {
            var result = await appDbContext.State
                .FirstOrDefaultAsync(s => s.Id == stateId);
            if (result != null)
            {
                appDbContext.State.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<State> GetState(int stateId)
        {
            return await appDbContext.State
               .FirstOrDefaultAsync(s => s.Id == stateId);
        }

        public async Task<State> GetStateByName(string name)
        {
            return await appDbContext.State
               .FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<IEnumerable<State>> GetStates()
        {
            return await appDbContext.State.Include(s => s.Results).ThenInclude(c => c.Candidate).ToListAsync();
        }
    }
}
