using Application.Features.Teknologies.Commands.CreateTeknology;
using Application.Features.Teknologies.Commands.DeleteTeknology;
using Application.Features.Teknologies.Commands.UpdateTeknology;
using Application.Features.Teknologies.Dtos;
using Application.Features.Teknologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teknologies.Profiles
{
    public class MappingProfiles  : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Teknology, CreateTeknologyCommand>().ReverseMap();

            CreateMap<Teknology, CreatedTeknologyDto>()
                .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();

            CreateMap<Teknology, TeknologyListDto>().ForMember(c => c.ProgrammingLanguageName, 
                opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();

            CreateMap<IPaginate<Teknology>, TeknologyListModel>().ReverseMap();

            CreateMap<Teknology, DeletedTeknologyDto>().ReverseMap();
            CreateMap<Teknology, DeleteTeknologyCommand>().ReverseMap();
            

            CreateMap<Teknology, UpdatedTeknologyDto>().ReverseMap();
            CreateMap<Teknology, UpdateTeknologyCommand>().ReverseMap();
        }


    }
}
