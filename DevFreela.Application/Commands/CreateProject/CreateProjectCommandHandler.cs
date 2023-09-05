using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectComand, int>
    {


        private readonly ProjectRepository _projectRepository;

        public CreateProjectCommandHandler( ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(CreateProjectComand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title
                                   , request.Description
                                   , request.IdClient
                                   , request.IdFreelancer
                                   , request.TotalCost);

            await _projectRepository.AddAsync(project);
  

            return project.Id;
        }
    }
}
