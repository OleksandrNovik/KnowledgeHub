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
            var newUser = CreateUserEntity(request);

            var jwt = GenerateToken(newUser);

            if (jwt is not null)
            {
                await userRepository.AddAsync(newUser, cancellationToken);

                var userInfo = mapper.Map<UserInfo>(newUser);
                var registrationResult = new UserAuthorizationResult(userInfo, jwt);

                return Result.Ok(registrationResult);
            }

            return ApiErrors.InternalApiError("Cloud not create jwt token for provided user identity");
        }

        return ApiErrors.BadRequest($"User with {request.Email} already exists.");
    }

    /// <summary>
    ///     Creates user entity and hashes password
    /// </summary>
    private UserEntity CreateUserEntity(UserRegistrationCommand request)
    {
        var user = mapper.Map<UserEntity>(request);
        user.Password = passwordHasher.HashPassword(user, request.Password);

        return user;
    }

    /// <summary>
    ///     Generates and returns jwt token for new user
    /// </summary>
    /// >
    private JwtToken? GenerateToken(UserEntity user)
    {
        var userIdentity = mapper.Map<UserIdentity>(user);
        return jwtService.GenerateJwtToken(userIdentity);
    }
}