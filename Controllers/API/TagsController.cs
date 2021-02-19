using Microsoft.AspNetCore.Mvc;
using TechInfoLookUp.Data.Entities;
using TechInfoLookUp.Data.Repositories;

namespace TechInfoLookUp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : BaseAPIController<Tag, int>
    {
        public override string EntityName => $"Thẻ {nameof(Tag)}";

        public TagsController(IAsyncRepository<Tag, int> repository) : base(repository) { }
    }
}
