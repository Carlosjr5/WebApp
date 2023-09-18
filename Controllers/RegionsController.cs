using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using static System.Net.WebRequestMethods;

namespace NZWalks.API.Controllers
{

    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
  
    public class RegionsController : ControllerBase
    {

        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        //Controller Constructor
        public RegionsController(NZWalksDbContext dbContext,
            IRegionRepository regionRepository, IMapper mapper )
        {
            //declaring database context.
            this.dbContext = dbContext;
            //Declaring region repository.
            this.regionRepository = regionRepository;
            //Mapping data.
            this.mapper = mapper;
        }

        // GET ALL REGIONS Hard asyncrunized
        // GET: https://localhost:PORT/api/regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {

            // Get data from database - Domain models
            var regionsDomain = await regionRepository.GetAllAsync();

            //Return mapped DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }

        //GET SINGLE REGION (Get Region By ID)
        //GET https://localhost/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
            {

            // var region = dbContext.Regions.Find(id);

            //Get region domain model from database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if(regionDomain == null)
            {
                return NotFound();
            }

            //return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
            }


        // POST To Create new Region
        // POST: https://localhost:PORT/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

                // Map or Convert DTO to Domain Model
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                //Use Dopmain Model to create REgion
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //Saving changes to see it reflect it.
                await dbContext.SaveChangesAsync();

                //Map Domain model back to DTO
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }


        //Update Data
        //PUT: https://localhost:PORT/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

                //Map DTO to Domain Model
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);


                //Check if region exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //Return Domain Model to DTO
                return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }


        // Delete Region Data
        // DELETE: https://localhost:PORT/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            
            //Calling Region Repository
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //return deleted Region back
            //mapping Domain Model to DTO.
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }




    }
}
