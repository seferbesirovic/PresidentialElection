using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Dtos;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> logger;
        private readonly ICandidateRepository candidateRepository;
        private readonly IStateRepository stateRepository;
        private readonly IResultRepository resultRepository;
        private readonly IResultService resultService;

        public FileController(
            ILogger<FileController> logger, 
            ICandidateRepository candidateRepository,
            IStateRepository stateRepository,
            IResultRepository resultRepository,
            IResultService resultService)
        {
            this.logger = logger;
            this.candidateRepository = candidateRepository;
            this.stateRepository = stateRepository;
            this.resultRepository = resultRepository;
            this.resultService = resultService;
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile([FromForm] IFormFile file)
        {   
            bool overrideFile = file.FileName.ToLower().Contains("override");
            try
            {
                if (file == null)
                    return BadRequest();
                var result = new StringBuilder();
                var reader = new StreamReader(file.OpenReadStream());
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
                
                String[] fileRows = result.ToString().Split("\r\n");
                
                for (int i = 0; i < fileRows.Length-1; i++)
                {
                    String[] rowsColumns = fileRows[i].Split(", ");
                    String stateName = rowsColumns[0].Trim();

                    if (stateName is null || stateName.Equals(""))
                    {
                        logger.LogWarning("State name is missing");
                        continue;
                    }
                    var state = await stateRepository.GetStateByName(stateName);
                    if (state == null)
                    {
                       state = await stateRepository.AddState(new State { Name = stateName });
                    }

                    for (int j = 1; j < rowsColumns.Length - 1; j += 2)
                    {
                        var candidateCode = rowsColumns[j + 1];

                        var candidate = await candidateRepository.GetCandidateByCode(candidateCode);
                        if(candidate == null)
                        {
                            logger.LogWarning($"Candidate with code = {candidateCode} doesn't exist");
                            continue;
                        }

                        int vote;
                        bool success = Int32.TryParse(rowsColumns[j], out vote);
                        if (!success || vote < 0)
                        {
                            logger.LogWarning("Number of votes needs to be positive integer");
                            success = false;
                        }
                        var existingResult = await resultRepository.GetResultsByCandidateIdAndStateId(candidate.Id, state.Id);
                        if (!overrideFile)
                        {
                            //In create mode we don't let the same result to be appeared.
                            if (existingResult != null)
                            {
                                logger.LogWarning("Result with the came candidateCode and stateName already exist");
                                continue;
                            }
                            await resultService.CreateResult(candidateCode, stateName, vote, success);
                        }
                        else
                        {
                            if (!success)
                                continue; // In update mode we don't update votes with errors
                            if (existingResult == null)
                            {
                                await resultService.CreateResult(candidateCode, stateName, vote, success);
                            }
                            else
                            {
                                await resultService.UpdateResult(candidateCode, stateName, vote, success);
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error while trying to upload file");
            }

        }
    }
}