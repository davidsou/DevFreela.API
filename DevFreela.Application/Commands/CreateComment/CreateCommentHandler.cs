using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly DevFreelaDbContext _dbContext;

        public CreateCommentHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);
            await _dbContext.ProjectComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
           // return Task.CompletedTask;
                    
        }
    }
}
