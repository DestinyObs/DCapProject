using DCapProject.Dtos;
using DCapProject.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCapProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public ProgramController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public IActionResult GetPrograms()
        {
            var programs = _cosmosDbService.GetPrograms();
            return Ok(programs);
        }

        [HttpPost]
        public IActionResult CreateProgram([FromBody] CreateProgramDTO createProgramDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProgram = _cosmosDbService.CreateProgram(createProgramDTO);
            return CreatedAtAction(nameof(GetProgram), new { id = createdProgram.Id }, createdProgram);
        }

        [HttpGet("{id}")]
        public IActionResult GetProgram(Guid id)
        {
            var program = _cosmosDbService.GetProgram(id);

            if (program == null)
            {
                return NotFound();
            }

            return Ok(program);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProgram(Guid id, [FromBody] UpdateProgramDTO updateProgramDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProgram = _cosmosDbService.UpdateProgram(id, updateProgramDTO);

            if (updatedProgram == null)
            {
                return NotFound();
            }

            return Ok(updatedProgram);
        }
    }

}
