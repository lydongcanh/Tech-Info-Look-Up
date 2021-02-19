using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechInfoLookUp.Data.Entities;
using TechInfoLookUp.Data.Repositories;
using TechInfoLookUp.Data.API.Tag;

namespace TechInfoLookUp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        protected readonly IAsyncRepository<Tag, int> repository;

        public TagsController(IAsyncRepository<Tag, int> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int skip = 0, int take = 50)
        {
            return Ok(await repository.GetAsync(skip, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await repository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TagPostRequest request)
        {
            return Ok(await repository.AddAsync(request.ToTag()));
        }
    }
}
