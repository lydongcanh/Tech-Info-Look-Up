using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechInfoLookUp.Data.Entities;
using TechInfoLookUp.Data.Repositories;
using TechInfoLookUp.Data.API.Tech;

namespace TechInfoLookUp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechsController : ControllerBase
    {
        protected readonly IAsyncRepository<Tech, int> techRepository;
        protected readonly IAsyncRepository<TechTag, Tuple<int, int>> techTagRepository;

        public TechsController(
            IAsyncRepository<Tech, int> techRepository,
            IAsyncRepository<TechTag, Tuple<int, int>> techTagRepository
        )
        {
            this.techRepository = techRepository;
            this.techTagRepository = techTagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int skip = 0, int take = 50)
        {
            return Ok(await techRepository.GetAsync(skip, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await techRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TechPostRequest request)
        {
            var addedTech = await techRepository.AddAsync(request.ToTech());
            if (addedTech == default(Tech))
                return BadRequest();

            if (request?.TagIds != null && request.TagIds.Count > 0)
            {
                List<TechTag> addedTechTags = new List<TechTag>();
                foreach (var tagId in request.TagIds)
                {
                    var addedTechTag = await techTagRepository.AddAsync(new TechTag()
                    {
                        TechId = addedTech.Id,
                        TagId = tagId
                    });
                    addedTechTags.Add(addedTechTag);
                }
                addedTech.TechTags = addedTechTags;
            }

            return Ok(addedTech);
        }
    }
}
