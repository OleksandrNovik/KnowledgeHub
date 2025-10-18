using FluentResults;
using KnowledgeHub.Domain.Dtos.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeHub.Api.Helpers;

public static class MiddlewareHelper
{
    /// <summary>
    ///     Invokes api command with typed return parameter.
    ///     Will check <see cref="Result" /> returned from command execution.
    ///     If <see cref="ApiError" /> has occured will return response with corresponding status code.
    ///     Other type of error will be threaded like internal server error.
    /// </summary>
    /// <param name="sender"> Requests sender that will send the command or query  </param>
    /// <param name="request">
    ///     Command or query that is sent to <see cref="IRequestHandler{TRequest,TResponse}" />
    /// </param>
    /// <typeparam name="TResult"> Return type of the </typeparam>
    /// <returns></returns>
    public static async Task<IResult> HandleAsync<TResult>(ISender sender, IRequest<Result<TResult>> request)
    {
        var result = await sender.Send(request);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        var error = result.Errors.FirstOrDefault();

        if (error is ApiError apiError)
        {
            return Results.Problem(new ProblemDetails
            {
                Detail = apiError.Message,
                Status = apiError.StatusCode
            });
        }

        return Results.InternalServerError(error?.Message ?? "An unexpected error occurred.");
    }
}