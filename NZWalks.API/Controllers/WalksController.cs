using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.WalkDTOs;
using NZWalks.API.Repositories.WalkRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {

        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        // Create a walk
        [HttpPost(Name = "CreateWalk")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateWalk([FromBody] CreateWalkRequestDTO model)
        {

            if(!ModelState.IsValid) return BadRequest(ModelState);

            var existingWalk = await _walkRepository.GetExistingWalkByName(model.Name);

            if (existingWalk != null) return Conflict("Walk already exist");

            var walkModel = _mapper.Map<Walk>(model);

            var walk = await _walkRepository.CreateWalkAsync(walkModel);

            var walkDto = _mapper.Map<WalkDTO>(walk);

            //return Ok(walkDto);
            return CreatedAtAction("GetWalkById", new { id = walkDto.Id }, walkDto);
        }

        // Get all walks
        [HttpGet(Name = "GetAllWalks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<WalkDTO>>> GetAllWalks()
        {
            var walksModels = await _walkRepository.GetAllWalksAsync();

            if (!walksModels.Any()) return NotFound("Walks not found");

            var walkDtos = _mapper.Map<IEnumerable<WalkDTO>>(walksModels);

            return Ok(walkDtos);
        }

        // Get a walk by its id
        [HttpGet("{id:Guid}", Name = "GetWalkById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<WalkDTO>> GetWalkById([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Invalid walk id");

            var walkModel = await _walkRepository.GetExistingWalkById(id);

            if (walkModel == null) return NotFound("Walk not found");

            var walkDto = _mapper.Map<WalkDTO>(walkModel);

            return Ok(walkDto);
        }

        // Update a walk
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<WalkDTO>> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO model)
        {

            if (id == Guid.Empty) return BadRequest("Invalid walk id");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var walkModel = _mapper.Map<Walk>(model);

            var updateWalk = await _walkRepository.UpdateWalksAsync(id, walkModel);

            if (updateWalk == null) return NotFound("Walk not found");

            var walkDto = _mapper.Map<WalkDTO>(updateWalk);

            return Ok(walkDto);
        }

        // Delete a walk
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteWalk([FromRoute] Guid id)
        {
            if(id == Guid.Empty) return BadRequest("Invalid walk id");

            var walkModel = await _walkRepository.GetExistingWalkById(id);

            if (walkModel == null) return NotFound("Walk not found");

            await _walkRepository.DeleteWalkAsync(walkModel);

            return Ok("Walk deleted successfully");
        }

    }
}
