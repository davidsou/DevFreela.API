using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title
                                    , inputModel.Description
                                    , inputModel.IdClient
                                    , inputModel.IdFreelancer
                                    , inputModel.TotalCost);

            _dbContext.Projects.Add(project);

            return project.Id;

        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
            _dbContext.ProjectComments.Add(comment);
        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project != null)
                project.Cancel();

        }

        public void Finish(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;

            var projectsVieModel = projects
                .Select(x => new ProjectViewModel(x.Title, x.CreatedAt))
                .ToList();

            return projectsVieModel
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            var projectDetailsViewModel = new ProjectDetailsViewModel(project.Id
                                                    , project.Title
                                                    , project.Description
                                                    ,project.TotalCost.GetValueOrDefault()
                                                    , project.StartedAt
                                                    , project.FinishedAt
                                                    );

            return projectDetailsViewModel;
        }

        public void Start(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            throw new NotImplementedException();
        }
    }
}
