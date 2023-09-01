using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly string _connectionString;

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public int Create(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title
                                    , inputModel.Description
                                    , inputModel.IdClient
                                    , inputModel.IdFreelancer
                                    , inputModel.TotalCost);

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return project.Id;

        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project != null)
            {
                project.Cancel();
                _dbContext.SaveChanges();
            }
             
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project != null)
            {
                project.Finish();
                _dbContext.SaveChanges();
            }
                
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;

            var projectsVieModel = projects
                .Select(x => new ProjectViewModel(x.Title, x.CreatedAt))
                .ToList();

            return projectsVieModel;
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects
                .Include(p=> p.Client)
                .Include(p=>p.Freelancer)
                .SingleOrDefault(p => p.Id == id);

            if(project==null)
            {
                return null;
            }

            var projectDetailsViewModel = new ProjectDetailsViewModel(project.Id
                                                    , project.Title
                                                    , project.Description
                                                    ,project.TotalCost.GetValueOrDefault()
                                                    , project.StartedAt
                                                    , project.FinishedAt
                                                    , project.Client.FullName
                                                    , project.Freelancer.FullName
                                                    );

            return projectDetailsViewModel;
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project != null)
            {
                project.Start();
               //_dbContext.SaveChanges();

                using( var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();   
                    var script = "UPDATE Projects Set Status = @status, StartedAt = @startedat Where Id = @Id";                }
              //  SqlConnection.Execute

            }
                
        }

        public void Update(UpdateProjectInputModel inputModel)
        {

            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
            _dbContext.SaveChanges();

        }
    }
}
