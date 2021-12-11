using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Dtos;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepository candidateRepository;
        public CandidateController(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }

        [HttpGet]
        public async Task<ActionResult<GetCandidateDto>> GetCandidates()
        {
            try
            {
                var result = (await candidateRepository.GetCandidates()).Select(candidate => candidate.AsDto());
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "error retrieving data from the database");
            }
            
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetCandidateDto>> GetCandidate(int id)
        {
            try
            {
                var result = await candidateRepository.GetCandidate(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result.AsDto();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error retrieving data from the database");
            }

        }

        [HttpGet("search")]
        public async Task<ActionResult<GetCandidateDto>> GetCandidateByCode([FromQuery] string code)
        {
            try
            {
                var result = await candidateRepository.GetCandidateByCode(code);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result.AsDto());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error retrieving data from the database");
            }

        }
    }
}
