using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SciqusTraining.API.CustomActionFilters;
using SciqusTraining.API.Data;
using SciqusTraining.API.Models.Domains;
using SciqusTraining.API.Models.DTO;
using SciqusTraining.API.Repositories;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SciqusTraining.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext,
            IRegionRepository regionRepository, 
            IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                throw new Exception("this is a custom exception");
                // Fetch Domain Models from the database
                var regionsDomain = await regionRepository.GetAllAsync();

                logger.LogInformation($"Finished GetAllRegion request with data: {JsonSerializer.Serialize(regionsDomain)}");

                return Ok(mapper.Map<List<RegionsController>>(regionsDomain));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message );
                throw;
            }

            
        }

        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Fetch Domain Model by ID from the database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //// Map Domain Model to DTO
            //var regionDto = new RegionDTO
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};
            var regionDto= mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDto);
        }

        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionReqDto addRegionReqDto)
        {
           
            // Map DTO to Domain Model
            //var regionDomainModel = new Region
            //{
            //    Code = addRegionReqDto.Code,
            //    Name = addRegionReqDto.Name,
            //    RegionImageUrl = addRegionReqDto.RegionImageUrl
            //};
            var regionDomainModel = mapper.Map<Region>(addRegionReqDto);

            // Add Domain Model to the database
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain Model back to DTO
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);

        }

        //Update
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionReqDto updateRegionReqDto)
        {
            
            //Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionReqDto);
            // Check if region exists
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
            return BadRequest(ModelState);
            
        }

        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Return deleted region back
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDto);
        }
    }
}
