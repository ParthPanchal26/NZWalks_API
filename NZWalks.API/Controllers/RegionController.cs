using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomeActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.RegionDTOs;
using NZWalks.API.Repositories.RegionRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {

        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        #region get
        //Get all regions
        [HttpGet(Name = "GetAllRegions")]
        [Authorize(Roles = "Reader")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RegionDTO>>> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllRegionsAsync();

            if (regions.Count == 0) return NotFound("Regions not found");

            var regionDTOs = _mapper.Map<IEnumerable<RegionDTO>>(regions);

            return Ok(regionDTOs);
        }

        // Get a region by its ID
        [HttpGet("{id:Guid}", Name = "GetRegionById")]
        [Authorize(Roles = "Reader")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegionDTO>> GetRegionById([FromRoute] Guid id)
        {

            if (id == Guid.Empty) return BadRequest("Invalid region id");

            var region = await _regionRepository.GetRegionAsync(id);

            if (region == null)
            {
                return NotFound("Region could not found");
            }

            var regionDTO = _mapper.Map<RegionDTO>(region);

            return Ok(regionDTO);
        }
        
        #endregion

        #region create
        // Crete a region
        [HttpPost(Name = "CreateRegion")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegionDTO>> CreateRegion([FromBody] CreateRegionRequestDTO model)
        {

            var exisingRegion = await _regionRepository.GetExisingRegion(model.Name, model.Code);

            if (exisingRegion != null) return Conflict("Region already exist");
   
            var regionModel = _mapper.Map<Region>(model);

            var region = await _regionRepository.CreateRegionAsync(regionModel);

            var regionDTO = _mapper.Map<RegionDTO>(region);

            return CreatedAtAction("GetRegionById", new { Id = regionDTO.Id }, regionDTO);
        }

        #endregion

        #region update
        // Update a region
        [HttpPut("{id:Guid}", Name = "UpdateRegion")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegionDTO>> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO model)
        {

            if (id == Guid.Empty) return BadRequest("Invalid region Id");

            var exisingRegion = await _regionRepository.GetRegionAsync(id);

            if (exisingRegion == null) return NotFound("Region not found");

            var dbRegion = await _regionRepository.UpdateRegionAsync(id, exisingRegion, model);

            var regionDTO = _mapper.Map<RegionDTO>(dbRegion);

            return Ok(regionDTO);
        }
        #endregion

        #region delete
        //Delete a region
        [HttpDelete("{id:Guid}", Name = "DeleteRegion")]
        [Authorize(Roles = "Writer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteRegion([FromRoute] Guid id)
        {

            if (id == Guid.Empty) return BadRequest("Invalid region id");

            var region = await _regionRepository.GetRegionAsync(id);

            if (region == null) return NotFound("Region not found");

            await _regionRepository.DeleteRegionAsync(region);

            return Ok("Region deleted successfully");
        }
        #endregion
    }
}
