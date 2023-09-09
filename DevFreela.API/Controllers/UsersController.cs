using Microsoft.AspNetCore.Mvc;
using DevFreela.API.Model;
using Microsoft.AspNetCore.Http;
using DevFreela.Application.ViewModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.Commands.CreateUser;
using MediatR;
using DevFreela.Application.Commands.LoginUser;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        public UsersController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        // api/users/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            //if (!ModelState.IsValid)
            //{
            //    var messages = ModelState
            //        .SelectMany(ms => ms.Value.Errors)
            //        .Select(e => e.ErrorMessage)
            //        .ToList();
            //    return BadRequest(messages);
            //}


            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        // api/users/login
        [HttpPut("login")]
        [AllowAnonymous]
        public async Task< IActionResult> Login( [FromBody] LoginUserCommand command)
        {
            // TODO: Para Módulo de Autenticação e Autorização
            var loginUserViewModel = await _mediator.Send(command);

            if (loginUserViewModel == null)
            {
                return BadRequest();
            }

            return Ok( loginUserViewModel);
        }
    }
}
