using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IStateRepository
    {
        Task<IEnumerable<State>> GetStates();
        Task<State> GetState(int stateId);
        Task<State> GetStateByName(string name);
        Task<State> AddState(State state);
        Task DeleteState(int stateId);
    }
}
