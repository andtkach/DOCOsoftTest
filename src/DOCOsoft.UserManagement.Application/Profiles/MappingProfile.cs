using AutoMapper;
using DOCOsoft.UserManagement.Application.Features.Users.Commands.CreateUser;
using DOCOsoft.UserManagement.Application.Features.Users.Commands.UpdateUser;
using DOCOsoft.UserManagement.Application.Features.Users.Queries.GetUserDetail;
using DOCOsoft.UserManagement.Application.Features.Users.Queries.GetUsersList;
using DOCOsoft.UserManagement.Domain.Entities;

namespace DOCOsoft.UserManagement.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserListVm>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<User, UserDetailVm>().ReverseMap();
        }
    }
}
