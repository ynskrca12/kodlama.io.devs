using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.CreatePL
{
    public class CreatePLCommandValidator : AbstractValidator<CreateProgrammingLanguageCommand>
    {
        public CreatePLCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
    
    
}
