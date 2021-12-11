using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Dtos;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultRepository resultRepository;
        private readonly IResultService resultService;
        public ResultController(IResultRepository resultRepository, IResultService resultService)
        {
            this.resultRepository = resultRepository;
            this.resultService = resultService;
        }

        [HttpGet]
        public async Task<ActionResult<GetResultDto>> GetResults()
        {
            try
            {
                var result = (await resultRepository.GetResults()).Select(result => result.AsDto());
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error retrieving data from the database");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetResultDto>> GetResult(int id)
        {
            try
            {
                var result = await resultRepository.GetResult(id);
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

        [HttpPost]
        public async Task<ActionResult> CreateResult(CreateOrUpdateResultDto createResultDto)
        {
            try
            {
                if (createResultDto == null)
                    return BadRequest();
                var result = (await resultService.CreateResult(
                    createResultDto.candidateCode,
                    createResultDto.stateName,
                    createResultDto.Votes)).AsDto();

                return CreatedAtAction(nameof(GetResult), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }

        }

        [HttpPatch]
        public async Task<ActionResult<GetResultDto>> UpdateResult(CreateOrUpdateResultDto updateResultDto)
        {
            try
            {
                if (updateResultDto == null)
                    return BadRequest();

                var result = await resultService.UpdateResult(
                    updateResultDto.candidateCode, 
                    updateResultDto.stateName, 
                    updateResultDto.Votes);

                return Ok(result.AsDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteResult(int id)
        {
            try
            {
                var existingResult = await resultRepository.GetResult(id);
                if (existingResult == null)
                {
                    return NotFound();
                }
                await resultRepository.DeleteResult(id);
                return Ok($"Result with Id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error deleting data from the database");
            }
        }



    }
}
