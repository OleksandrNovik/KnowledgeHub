using FluentResults;
using KnowledgeHub.Domain.Dtos.User.Authorization;
using MediatR;

namespace KnowledgeHub.Application.User.Authorization;

public record UserAuthorizationCommand(string Email, string Password)
    : IRequest<Result<UserAuthorizationResult>>;