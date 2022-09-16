using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Command.UserLogin
{
    public class UserLoginCommand : UserForLoginDto, IRequest<AccessToken>
    {
    }
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, AccessToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly UserBusinessRules _userBusinessRules;
        public UserLoginCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _userOperationClaimRepository = userOperationClaimRepository;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<AccessToken> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserLoginEmailCheck(request.Email);

            User user = await _userRepository.GetAsync(u => u.Email == request.Email);
            if (!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException("The password you entered is incorrect.");

            IPaginate<UserOperationClaim> userGetClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id,
                include: i => i.Include(i => i.OperationClaim));

            AccessToken accessToken = _tokenHelper.CreateToken(user, userGetClaims.Items.Select(u => u.OperationClaim).ToList());

            return accessToken;
        }
    }
}
