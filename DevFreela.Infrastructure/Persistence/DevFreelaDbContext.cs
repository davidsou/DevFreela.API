using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {

        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("My aspnet core project 1.","my description 1" ,1,1,10000),
                new Project("My aspnet core project 2.","my description 2" ,1,1,20000),
                new Project("My aspnet core project 3.","my description 3" ,1,1,30000),
            };

            Users = new List<User>
            {
                new User("David Soares","davidsou@gmail.com",new DateTime(1979,4,27)),
                new User("David Soares","maria@gmail.com",new DateTime(1949,7,29)),
                new User("David Soares","jose@gmail.com",new DateTime(1929,3,7))
            };

            Skills = new List<Skill>
            {
                new Skill(".net"),
                new Skill("C#"),
                new Skill("SQL")
            };
        }

        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }

        public List<ProjectComment> ProjectComments { get; set; }

    }
}
