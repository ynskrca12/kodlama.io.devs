using Application.Features.ProgrammingLanguages.Commands.CreatePL;
using Application.Features.ProgrammingLanguages.Commands.DeletePL;
using Application.Features.ProgrammingLanguages.Commands.UpdatePL;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProgrammingLanguage, CreatedProgrammingLanguageDto>().ReverseMap();
            CreateMap<ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();

            CreateMap<ProgrammingLanguage, DeleteProgrammingLanguageCommand>().ReverseMap();
            CreateMap<ProgrammingLanguage, DeletedProgrammingLanguageDto>().ReverseMap();

            CreateMap<ProgrammingLanguage, UpdateProgrammingLanguageCommand>().ReverseMap();
            CreateMap<ProgrammingLanguage, UpdatedProgrammingLanguageDto>().ReverseMap();

            CreateMap<IPaginate<ProgrammingLanguage>, PLListModel>().ReverseMap();

            CreateMap<ProgrammingLanguage, PLListDto>().ReverseMap();
            CreateMap<ProgrammingLanguage, PLGetByIdDto>().ReverseMap();
        }
    }
}
