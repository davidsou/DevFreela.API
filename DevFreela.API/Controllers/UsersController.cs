using Microsoft.AspNetCore.Mvc;
using DevFreela.API.Model;
using Microsoft.AspNetCore.Http;


namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserModel createUserModel)
        {
            return CreatedAtAction(nameof(GetById), new {id=1}, createUserModel);
        }
        [HttpPut("{id}/login")]
        public IActionResult Login(int  id, [FromBody]  LoginModel   login) 
        {
            return NoContent();
        }
    }
}
