﻿using FluentValidation;
using Login.Domain.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;


            RuleFor(x => x.Nome)
           .NotEmpty().WithMessage("Informe o nome")
           .MinimumLength(5).WithMessage("O nome deve ter no mínimo 5 caracteres")
           .MaximumLength(150).WithMessage("O nome deve ter no máximo 150 caracteres")
           .DependentRules(() =>
           {
               RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Informe o E-mail")

                .DependentRules(() =>
                {
                        RuleFor(x => x.Senha)
                        .NotEmpty().WithMessage("Informe a senha");
                });
           });
        }
    }
}
