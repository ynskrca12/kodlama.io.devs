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

namespace Application.Features.Teknologies.Commands.DeleteTeknology
{
    public class DeleteTeknologyCommand : IRequest<DeletedTeknologyDto>
    {
        public int Id { get; set; }
        public class DeleteTeknologyHandler : IRequestHandler<DeleteTeknologyCommand, DeletedTeknologyDto>
        {
            private readonly ITeknologyRepository _teknologyRepository;
            private readonly IMapper _mapper;
            

            public DeleteTeknologyHandler(ITeknologyRepository teknologyRepository, IMapper mapper)
            {
                _teknologyRepository = teknologyRepository;
                _mapper = mapper;
               
            }

            public async Task<DeletedTeknologyDto> Handle(DeleteTeknologyCommand request, CancellationToken cancellationToken)
            {

                Teknology? teknology = await _teknologyRepository.GetAsync(e => e.Id == request.Id);          

                await _teknologyRepository.DeleteAsync(teknology);
                DeletedTeknologyDto deletedTeknologyDto = _mapper.Map<DeletedTeknologyDto>(teknology);

                return deletedTeknologyDto;

            }
        }
    }
}
