using FluentResults;

namespace KnowledgeHub.Domain.Dtos.Errors;

public class ApiError(int statusCode, string message) : Error(message)
{
    public int StatusCode { get; } = statusCode;
}