using DevFreela.API.Model;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly OpeningTimeOption _option;
        private readonly IProjectService _projectService;
        private readonly IMediator _mediator;
        public ProjectsController(IOptions<OpeningTimeOption> option, IProjectService projectService, IMediator mediator)
        {
            _option = option.Value;
            _projectService = projectService;
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult Get(string query)
        {
            var projetcs = _projectService.GetAll(query);

            return Ok(projetcs);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] NewProjectInputModel inputModel)
        //{
        //    if (inputModel.Title.Length > 50)
        //    {
        //        return BadRequest();
        //    }
        //    var id = _projectService.Create(inputModel);

        //    return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        //}

        /// <summary>
        /// Using Mediator
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProjectComand command)
        {
            if (command.Title.Length > 50)
            {
                return BadRequest();
            }
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel inputModel)
        {
            if (inputModel.Description.Length > 50)
            {
                return BadRequest();
            }

            _projectService.Update(inputModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projectService.Delete(id);
            return NoContent();
        }

        //[HttpPost("{id}/comments")]
        //public IActionResult PostComment(int id, [FromBody] CreateCommentInputModel inputModel)
        //{
        //    _projectService.CreateComment(inputModel);
        //    return NoContent();
        //}

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);

            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            _projectService.Finish(id);
            return NoContent();
        }
    }

}
