using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetListPL;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Queries.GetByIdPL
{
    public class GetByIdPLQuery : IRequest<PLGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdPLQueryHandler : IRequestHandler<GetByIdPLQuery, PLGetByIdDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly PLBusinessRules _pLBusinessRules;



            public GetByIdPLQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, PLBusinessRules pLBusinessRules)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
                _pLBusinessRules = pLBusinessRules;
            }

            public async Task<PLGetByIdDto> Handle(GetByIdPLQuery request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(b => b.Id == request.Id);

                _pLBusinessRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);

                PLGetByIdDto pLGetByIdDto = _mapper.Map<PLGetByIdDto>(programmingLanguage);
                return pLGetByIdDto;
            }
        }
    }
}
