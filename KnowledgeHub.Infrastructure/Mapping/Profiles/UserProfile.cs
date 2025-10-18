using AutoMapper;
using KnowledgeHub.Application.User.Registration;
using KnowledgeHub.Domain.Dtos.User.Authorization;
using KnowledgeHub.Domain.Entities.User;

namespace KnowledgeHub.Infrastructure.Mapping.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegistrationCommand, UserEntity>();
        CreateMap<UserEntity, UserIdentity>();
        CreateMap<UserEntity, UserInfo>();
    }
}