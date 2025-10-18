using FluentResults;
using KnowledgeHub.Application.Repositories;
using KnowledgeHub.Application.Services.User;
using MediatR;

namespace KnowledgeHub.Infrastructure.Services.User.Registration;

public class UserRegistrationHandler(IUserRepository userRepository, IJwtService jwtService) :
    IRequestHandler<UserRegistrationCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (existingUser == null)
        {
        }

        return Result.Fail<string>($"User with email {request.Email} already exists.");
    }
}