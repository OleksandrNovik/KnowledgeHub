using FluentResults;
using KnowledgeHub.Domain.Dtos.Errors;
using Microsoft.AspNetCore.Http;

namespace KnowledgeHub.Application.Helpers;

public static class ApiErrors
{
    /// <summary>
    ///     Creates <see cref="ApiError" /> with 400 status code (bad request)
    /// </summary>
    /// <param name="message"> Error message provided for <see cref="ApiError" /> </param>
    public static Result BadRequest(string message)
    {
        return Result.Fail(new ApiError(StatusCodes.Status400BadRequest, message));
    }

    public static Result InternalApiError(string message)
    {
        return Result.Fail(new ApiError(StatusCodes.Status500InternalServerError, message));
    }

    public static Result NotFound(string message)
    {
        return Result.Fail(new ApiError(StatusCodes.Status404NotFound, message));
    }
}