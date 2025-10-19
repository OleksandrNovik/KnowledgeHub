using AutoMapper;
using FluentResults;
using KnowledgeHub.Application.Helpers;
using KnowledgeHub.Application.Repositories;
using KnowledgeHub.Application.Services.User;
using KnowledgeHub.Application.User.Abstract;
using KnowledgeHub.Domain.Dtos.User.Authorization;
using KnowledgeHub.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeHub.Application.User.Authorization;

public class UserAuthorizationHandler(
    IUserRepository userRepository,
    IJwtService jwtService,
    IMapper mapper,
    IPasswordHasher<UserEntity> passwordHasher) :
    BaseAuthorizationResultHandler(jwtService, mapper),
    IRequestHandler<UserAuthorizationCommand, Result<UserAuthorizationResult>>
{
    public async Task<Result<UserAuthorizationResult>> Handle(UserAuthorizationCommand request,
        CancellationToken ct)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Email, ct);

        if (user is not null)
        {
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (verificationResult is PasswordVerificationResult.Failed)
            {
                return ApiErrors.BadRequest("User password is incorrect");
            }

            var jwt = GenerateToken(user);

            if (jwt is not null)
            {
                var authorizationResult = GetAuthorizationResult(user, jwt);
                return Result.Ok(authorizationResult);
            }

            return ApiErrors.InternalApiError("Cloud not create jwt token for provided user identity");
        }

        return ApiErrors.NotFound($"User with email {request.Email} not found");
    }
}