using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomeActionFilters;
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

        #region create
        // Create a walk
        [HttpPost(Name = "CreateWalk")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateWalk([FromBody] CreateWalkRequestDTO model)
        {
            var existingWalk = await _walkRepository.GetExistingWalkByName(model.Name);

            if (existingWalk != null) return Conflict("Walk already exist");

            var walkModel = _mapper.Map<Walk>(model);

            var walk = await _walkRepository.CreateWalkAsync(walkModel);

            var walkDto = _mapper.Map<WalkDTO>(walk);

            var newWalkDto = await _walkRepository.GetExistingWalkById(walkDto.Id);

            return CreatedAtAction("GetWalkById", new { id = newWalkDto.Id }, newWalkDto);
        }
        #endregion

        #region get
        // Get all walks
        [HttpGet(Name = "GetAllWalks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<WalkDTO>>> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool isAscending)
        {
            var walksModels = await _walkRepository.GetAllWalksAsync(filterOn, filterQuery, sortBy, isAscending);

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
        #endregion

        #region put
        // Update a walk
        [HttpPut("{id:Guid}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<WalkDTO>> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO model)
        {

            if (id == Guid.Empty) return BadRequest("Invalid walk id");

            var walkModel = _mapper.Map<Walk>(model);

            var updateWalk = await _walkRepository.UpdateWalksAsync(id, walkModel);

            if (updateWalk == null) return NotFound("Walk not found");

            var walkDto = _mapper.Map<WalkDTO>(updateWalk);

            return Ok(walkDto);
        }
        #endregion

        #region delete
        // Delete a walk
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteWalk([FromRoute] Guid id)
        {
            if(id == Guid.Empty) return BadRequest("Invalid walk id");

            var walkModel = await _walkRepository.GetExistingWalkById(id);

            if (walkModel == null) return NotFound("Walk not found");

            await _walkRepository.DeleteWalkAsync(walkModel);

            return Ok("Walk deleted successfully");
        }
        #endregion

    }
}
