﻿using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Command.UserRegister
{
    public class UserRegisterCommand : UserForRegisterDto, IRequest<AccessToken>
    {
    }
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, AccessToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;
        private readonly UserBusinessRules _userBusinessRules;

        public UserRegisterCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
            _userBusinessRules = userBusinessRules;
        }
        public async Task<AccessToken> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserRegisterNameCanNotBeDuplicatedWhenInserted(request.Email);

            Byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            User user = _mapper.Map<User>(request);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Status = true;

            User newUser = await _userRepository.AddAsync(user);
            var token = _tokenHelper.CreateToken(newUser, new List<OperationClaim>());
            return token;
        }
    }
}
