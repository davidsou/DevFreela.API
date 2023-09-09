using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("E-mail não valido!");

            RuleFor(p => p.Password)
                .Must(ValidPassword)
                .WithMessage(@"Senha deve conter pelo menos 8 caracteres,
                        um número, uma letra maiuscula e um caracter especial");

            RuleFor(p => p.FullName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome é obrigatório!");
        }

        public bool ValidPassword (string password)
        {
            //Todo incluir regex.
            return true;
        }
    }
}
