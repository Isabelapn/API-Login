using FluentValidation;
using Login.Domain.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Validators
{
    public class LoginUpdateValidator : AbstractValidator<LoginUpdateRequest>
    {
        public LoginUpdateValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.IdUsers)
           .NotNull().WithMessage("Informe o Id")//Não pode ser nulo
           .NotEmpty().WithMessage("informe o Id")//não pode vazio
           .GreaterThan(0).WithMessage("Informe o nome"); //Não pode zero
        }
    }
}
