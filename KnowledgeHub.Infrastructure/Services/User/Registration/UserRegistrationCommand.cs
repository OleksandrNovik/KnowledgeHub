using FluentResults;
using MediatR;

namespace KnowledgeHub.Infrastructure.Services.User.Registration;

public record UserRegistrationCommand(string Username, string Email, string Password)
    : IRequest<Result<string>>;