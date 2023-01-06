using AutoMapper;
using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Authorization.Inputs;
using StaffWork.Server.GraphQL.Authorization.Types;
using StaffWork.Server.GraphQL.User.Inputs;
using StaffWork.Server.GraphQL.Ware.Inputs;
using StaffWork.Server.JwtAuthorization;

namespace StaffWork.Server
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<NewUserModel, UserModel>().ReverseMap();
            CreateMap<NewUserModel, CreateUserInput>().ReverseMap();
            CreateMap<UserLoginInput, UserLoginModel>().ReverseMap();
            CreateMap<SignInQueryResponse, AuthenticationResponseModel>().ReverseMap();
            CreateMap<NewWareModel, CreateWareInput>().ReverseMap();
        }
    }
}
