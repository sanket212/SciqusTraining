using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SciqusTraining.API.CustomActionFilters;
using SciqusTraining.API.Models.Domains;
using SciqusTraining.API.Models.DTO;
using SciqusTraining.API.Repositories;

namespace SciqusTraining.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        //Create Walk
        //POST: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkReqDto addWalkReqDto)
        {
            
            //Map DTO to domain model
            var walkDomainModel = mapper.Map<Walk>(addWalkReqDto);
            await walkRepository.CreateAsync(walkDomainModel);


            // Map Domain model to DTO
            return Ok(mapper.Map<DifficultyDTO>(walkDomainModel));
        }



        //Get walks
        //GET" /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, 
            [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,
                pageSize, pageNumber);

            // create an exception 
            throw new Exception("This is a new Exception");

            // Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDTO>>(walksDomainModel));
        }

        //Get by id
        //GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walkDomaiModel = await walkRepository.GetByIdAsync(id);
            if (walkDomaiModel == null) { return NotFound(); }
            //Map doain Model to Dto
            return Ok(mapper.Map<WalkDTO>(walkDomaiModel));
        }

        //Update by id
        //PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateWalkReqDto updateWalkReqDto)
        {
            //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkReqDto);
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null) { return NotFound(); }
            //Map Domain Model to DTO
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
            
        }

        //Delete by id
        //DELETE: /api/Walk/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var deleteWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deleteWalkDomainModel == null) { return NotFound(); }
            //Map Domain Model to DTO 
            return Ok(mapper.Map<WalkDTO>(deleteWalkDomainModel));
        }
    }
}
