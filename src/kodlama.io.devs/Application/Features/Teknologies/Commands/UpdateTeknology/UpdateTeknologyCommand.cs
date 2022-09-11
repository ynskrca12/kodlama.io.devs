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

namespace Application.Features.Teknologies.Commands.UpdateTeknology
{
    public class UpdateTeknologyCommand : IRequest<UpdatedTeknologyDto>
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public class UpdateTeknologyHandler : IRequestHandler<UpdateTeknologyCommand, UpdatedTeknologyDto>
        {
            private readonly ITeknologyRepository _teknologyRepository;
            private readonly IMapper _mapper;
          

            public UpdateTeknologyHandler(ITeknologyRepository teknologyRepository, IMapper mapper)
            {
                _teknologyRepository = teknologyRepository;
                _mapper = mapper;
               
            }

            public async Task<UpdatedTeknologyDto> Handle(UpdateTeknologyCommand request, CancellationToken cancellationToken)
            {             

                Teknology mappedTeknology = _mapper.Map<Teknology>(request);
                Teknology updatedTeknology = await _teknologyRepository.UpdateAsync(mappedTeknology);
                UpdatedTeknologyDto updatedTeknologyDto = _mapper.Map<UpdatedTeknologyDto>(updatedTeknology);

                return updatedTeknologyDto;
            }
        }
    }
}
