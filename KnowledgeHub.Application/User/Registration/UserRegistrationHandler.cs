using AutoMapper;
using FluentResults;
using KnowledgeHub.Application.Helpers;
using KnowledgeHub.Application.Repositories;
using KnowledgeHub.Application.Services.User;
using KnowledgeHub.Domain.Dtos.User.Authorization;
using KnowledgeHub.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeHub.Application.User.Registration;

public class UserRegistrationHandler(
    IUserRepository userRepository,
    IJwtService jwtService,
    IMapper mapper,
    IPasswordHasher<UserEntity> passwordHasher) :
    IRequestHandler<UserRegistrationCommand, Result<UserAuthorizationResult>>
{
    /// <inheritdoc />
    public async Task<Result<UserAuthorizationResult>> Handle(UserRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (existingUser is null)
        {
            var newUser = mapper.Map<UserEntity>(request);
            newUser.Password = passwordHasher.HashPassword(newUser, request.Password);
            await userRepository.AddAsync(newUser, cancellationToken);

            var userIdentity = mapper.Map<UserIdentity>(newUser);
            var jwt = jwtService.GenerateJwtToken(userIdentity);

            if (jwt is not null)
            {
                var userInfo = mapper.Map<UserInfo>(newUser);
                var registrationResult = new UserAuthorizationResult(userInfo, jwt);

                return Result.Ok(registrationResult);
            }

            return ApiErrors.InternalApiError("Cloud not create jwt token for provided user identity");
        }

        return ApiErrors.BadRequest($"User with {request.Email} already exists.");
    }
}