﻿using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {           
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");

        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }
    }
}