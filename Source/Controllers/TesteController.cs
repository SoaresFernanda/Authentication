using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        public IActionResult TestedeAutorizacao()
        {
            var teste = new test3() { id = 1, name = "Autorização realizada com sucesso" };
            return StatusCode(200, teste);
        }



        public class test3
        {
            public int id { get; set; }
            public string? name { get; set; }

        }
    }
}
