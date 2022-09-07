using Application.Features.ProgrammingLanguages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Queries.GetListPL
{
    public class GetListPLQuery : IRequest<PLListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListPLQueryHandler : IRequestHandler<GetListPLQuery, PLListModel>
        {
           private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
           private readonly IMapper _mapper;

            public GetListPLQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
            }

            public async Task<PLListModel> Handle(GetListPLQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingLanguage> programmingLanguages = await _programmingLanguageRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                PLListModel mappedPLListModel = _mapper.Map<PLListModel>(programmingLanguages);
                return mappedPLListModel;
            }
        }
    }
}
