using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
         //Utilizar o mesmo algoritmo para criar o hash da senha
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            // Buscar no meu banco de ados um user que tenha meu e-amil e minha senha no formato hash
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);
            //Se não existir  erro no login

            if(user == null)
            {
                return null;
            }
            // Se existir gera o tokem  retorna os dados para o usuário
            var token = _authService.GenerateJwtToken(user.Email, user.Role);

            return  new LoginUserViewModel(user.Email, token);
        }
    }
}
