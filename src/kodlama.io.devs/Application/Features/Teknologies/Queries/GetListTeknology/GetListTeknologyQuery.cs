using Application.Features.Teknologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teknologies.Queries.GetListTeknology
{
    public class GetListTeknologyQuery : IRequest<TeknologyListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListTeknologyQueryHandler : IRequestHandler<GetListTeknologyQuery, TeknologyListModel>
        {
            private readonly IMapper _mapper;
            private readonly ITeknologyRepository _teknologyRepository;

            public GetListTeknologyQueryHandler(IMapper mapper, ITeknologyRepository teknologyRepository)
            {
                _mapper = mapper;
                _teknologyRepository = teknologyRepository;
            }

            public async Task<TeknologyListModel> Handle(GetListTeknologyQuery request, CancellationToken cancellationToken)
            {
               IPaginate<Teknology> teknologies = await _teknologyRepository.GetListAsync(include: 
                    m => m.Include(c => c.ProgrammingLanguage),
                    index:request.PageRequest.Page,
                    size:request.PageRequest.PageSize
                    );

                TeknologyListModel mappedTeknology = _mapper.Map<TeknologyListModel>(teknologies);

                return mappedTeknology;
            }
        }
    }
}
