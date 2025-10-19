using AutoMapper;
using KnowledgeHub.Application.Services.User;
using KnowledgeHub.Domain.Dtos.User.Authorization;
using KnowledgeHub.Domain.Entities.User;

namespace KnowledgeHub.Application.User.Abstract;

public abstract class BaseAuthorizationResultHandler(IJwtService jwtService, IMapper mapper)
{
    protected readonly IJwtService JwtService = jwtService;
    protected readonly IMapper Mapper = mapper;

    /// <summary>
    ///     Generates and returns jwt token for new user
    /// </summary>
    protected JwtToken? GenerateToken(UserEntity user)
    {
        var userIdentity = Mapper.Map<UserIdentity>(user);
        return JwtService.GenerateJwtToken(userIdentity);
    }

    /// <summary>
    ///     Creates authorization result based on created <see cref="UserEntity" /> and generated<see cref="JwtToken" />
    /// </summary>
    protected UserAuthorizationResult GetAuthorizationResult(UserEntity user, JwtToken token)
    {
        var userInfo = Mapper.Map<UserInfo>(user);
        return new UserAuthorizationResult(userInfo, token);
    }
}