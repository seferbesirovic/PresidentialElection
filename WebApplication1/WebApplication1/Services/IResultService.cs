using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IResultService
    {
        public Task<Result> CreateResult(string candidateCode, string stateName, int votes);
        public Task<Result> UpdateResult(string candidateCode, string stateName, int votes);
        public Task<Result> CreateResult(string candidateCode, string stateName, int votes, bool success);
        public Task<Result> UpdateResult(string candidateCode, string stateName, int votes, bool success);

    }
}
