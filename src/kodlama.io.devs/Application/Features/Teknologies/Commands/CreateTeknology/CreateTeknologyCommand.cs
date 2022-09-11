using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.Teknologies.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teknologies.Commands.CreateTeknology
{
    public class CreateTeknologyCommand : IRequest<CreatedTeknologyDto>
    {
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public class CreateTeknologyHandler : IRequestHandler<CreateTeknologyCommand, CreatedTeknologyDto>
        {
            private readonly ITeknologyRepository _teknologyRepository;
            private readonly IMapper _mapper;
           // private readonly PLBusinessRules _pLBusinessRules;
           private readonly IProgrammingLanguageRepository _programmingLanguageRepository;  

            public CreateTeknologyHandler(ITeknologyRepository teknologyRepository, IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository)
            {
                _teknologyRepository = teknologyRepository;
                _mapper = mapper;
                //_pLBusinessRules = pLBusinessRules;
                _programmingLanguageRepository = programmingLanguageRepository;
            }

            public async Task<CreatedTeknologyDto> Handle(CreateTeknologyCommand request, CancellationToken cancellationToken)
            {
               // await _pLBusinessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(request.Name);

                Teknology mappedTeknology = _mapper.Map<Teknology>(request);
                Teknology createdTeknology = await _teknologyRepository.AddAsync(mappedTeknology);

                createdTeknology.ProgrammingLanguage =
                    await _programmingLanguageRepository.GetAsync(p => p.Id == createdTeknology.ProgrammingLanguageId);



                CreatedTeknologyDto createdTeknologyDto = _mapper.Map<CreatedTeknologyDto>(createdTeknology);
                return createdTeknologyDto;
            }
        }
    }
}
