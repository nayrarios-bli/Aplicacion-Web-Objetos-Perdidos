using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Core.Enums;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController() 
        {
            
        }
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpGet()]
        public async Task<IActionResult> GetTest()
        {
            return Task;
        }
    }
}
