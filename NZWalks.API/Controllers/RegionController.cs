using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        //Get all regions
        [HttpGet(Name = "GetAllRegions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RegionDTO>>> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllRegionsAsync();

            if (regions.Count == 0) return NotFound("Regions not found");

            //var regionDTOs = regions.Select(r => new RegionDTO()
            //{
            //    Id = r.Id,
            //    Name = r.Name,
            //    Code = r.Code,
            //    RegionImageUrl = r.RegionImageUrl,
            //});

            var regionDTOs = _mapper.Map<IEnumerable<RegionDTO>>(regions);

            return Ok(regionDTOs);
        }

        // Get a region by its ID
        [HttpGet("{id:Guid}", Name = "GetRegionById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegionDTO>> GetRegionById([FromRoute] Guid id)
        {
            //var region = _dbContext.Regions.Where(r => r.Id == id).FirstOrDefault();

            if (id == Guid.Empty) return BadRequest("Invalid region id");

            var region = await _regionRepository.GetRegionAsync(id);

            if (region == null)
            {
                return NotFound("Region could not found");
            }

            //var regionDTO = new RegionDTO()
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl,
            //};

            var regionDTO = _mapper.Map<RegionDTO>(region);

            return Ok(regionDTO);
        }

        // Crete a region
        [HttpPost(Name = "CreateRegion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegionDTO>> CreateRegion([FromBody] CreateRegionRequestDTO model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exisingRegion = await _regionRepository.GetExisingRegion(model.Name, model.Code);

            if (exisingRegion != null) return Conflict("Region already exist");

            //var regionModel = new Region()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = model.Name,
            //    Code = model.Code,
            //    RegionImageUrl = model.RegionImageUrl
            //};

            var regionModel = _mapper.Map<Region>(model);

            var region = await _regionRepository.CreateRegionAsync(regionModel);

            //var regionDTO = new RegionDTO()
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};

            var regionDTO = _mapper.Map<RegionDTO>(region);

            return CreatedAtAction("GetRegionById", new { Id = regionDTO.Id }, regionDTO);
        }

        // Update a region
        [HttpPut("{id:Guid}", Name = "UpdateRegion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegionDTO>> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO model)
        {

            if (id == Guid.Empty) return BadRequest("Invalid region Id");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exisingRegion = await _regionRepository.GetRegionAsync(id);

            if (exisingRegion == null) return NotFound("Region not found");

            var dbRegion = await _regionRepository.UpdateRegionAsync(id, exisingRegion, model);

            //var regionDTO = new RegionDTO()
            //{
            //    Id = dbRegion.Id,
            //    Code = dbRegion.Code,
            //    Name = dbRegion.Name,
            //    RegionImageUrl = dbRegion.RegionImageUrl
            //};

            var regionDTO = _mapper.Map<RegionDTO>(dbRegion);

            return Ok(regionDTO);
        }

         //Delete a region
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    }
}
