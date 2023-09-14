﻿using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTests
    {
        [Fact]
       public void TestIfProjectStartWorks()
        {
            var project = new Project(
                "Nome de Teste",
                "Descrição de teste",
                1,
                2,
                10000
                );

            Assert.Equal(ProjectStatusEnum.Created, project.Status);
            Assert.NotNull(project.CreatedAt);

            Assert.NotNull(project.Title);
            Assert.NotEmpty(project.Title);

            Assert.NotNull(project.Description);
            Assert.NotEmpty(project.Description);

            project.Start();

            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);

            
        }
    }
}