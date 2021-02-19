using Microsoft.AspNetCore.Mvc;
using TechInfoLookUp.Data.Entities;
using TechInfoLookUp.Data.Repositories;

namespace TechInfoLookUp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechsController : BaseAPIController<Tech, int>
    {
        public override string EntityName => $"Công nghệ {nameof(Tech)}";

        public TechsController(IAsyncRepository<Tech, int> repository) : base(repository) { }
    }
}
