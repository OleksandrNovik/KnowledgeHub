using FluentResults;
using KnowledgeHub.Domain.Dtos.User.Authorization;
using MediatR;

namespace KnowledgeHub.Application.User.Registration;

public record UserRegistrationCommand(string Username, string Email, string Password)
    : IRequest<Result<UserAuthorizationResult>>;