using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Dtos;
using System.Linq;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository stateRepository;
        public StateController(IStateRepository stateRepository)
        {
            this.stateRepository = stateRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetStates()
        {
            try
            {
                var result = (await stateRepository.GetStates()).Select(state => state.AsDto());
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error retrieving data from the database");
            }

        }

        [HttpGet("formatted")]
        public async Task<ActionResult> GetStatesFormatted()
        {
            try
            {
                var result = (await stateRepository.GetStates()).Select(state => state.AsFormattedDto());
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error retrieving data from the database");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetStateDto>> GetState(int id)
        {
            try
            {
                var result = await stateRepository.GetState(id);
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
        public async Task<ActionResult<GetCandidateDto>> GetStateByName([FromQuery] string name)
        {
            try
            {
                var result = await stateRepository.GetStateByName(name);
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

        [HttpPost]
        public async Task<ActionResult> CreateState(CreateStateDto createStateDto)
        {
            try
            {
                if (createStateDto == null)
                    return BadRequest();

                State state = new State
                {
                    Name = createStateDto.Name,
                };

                var createdState = (await stateRepository.AddState(state)).AsDto();
                return CreatedAtAction(nameof(GetState), new { id = createdState.Id }, createdState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.InnerException.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteState(int id)
        {
            try
            {
                var existingState = await stateRepository.GetState(id);
                if (existingState == null)
                {
                    return NotFound();
                }
                await stateRepository.DeleteState(id);
                return Ok($"State with Id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error deleting data from the database");
            }
        }

    }
}
