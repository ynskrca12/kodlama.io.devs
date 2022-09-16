using Application.Features.Users.Command;
using Application.Features.Users.Command.UserLogin;
using Application.Features.Users.Command.UserRegister;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserRegisterCommand>().ReverseMap();
            CreateMap<User, UserLoginCommand>().ReverseMap();
        }
    }
}
