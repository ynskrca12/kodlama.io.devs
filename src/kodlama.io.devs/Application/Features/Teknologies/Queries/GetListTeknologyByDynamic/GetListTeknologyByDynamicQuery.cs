using Application.Features.Teknologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teknologies.Queries.GetListTeknologyByDynamic
{
    public class GetListTeknologyByDynamicQuery : IRequest<TeknologyListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListTeknologyByDynamicQueryHandler : IRequestHandler<GetListTeknologyByDynamicQuery, TeknologyListModel>
        {
            private readonly IMapper _mapper;
            private readonly ITeknologyRepository _teknologyRepository;

            public GetListTeknologyByDynamicQueryHandler(IMapper mapper, ITeknologyRepository teknologyRepository)
            {
                _mapper = mapper;
                _teknologyRepository = teknologyRepository;
            }

            public async Task<TeknologyListModel> Handle(GetListTeknologyByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Teknology> teknologies = await _teknologyRepository.GetListByDynamicAsync(request.Dynamic, 
                     include:
                     m => m.Include(c => c.ProgrammingLanguage),
                     index: request.PageRequest.Page,
                     size: request.PageRequest.PageSize
                     );

                TeknologyListModel mappedTeknology = _mapper.Map<TeknologyListModel>(teknologies);

                return mappedTeknology;
            }
        }
    }
}
