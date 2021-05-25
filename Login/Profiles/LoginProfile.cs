using Login.Data.Entities;
using Login.Domain.Models.Request;
using Login.Domain.Models.Response;
using AutoMapper;


namespace Login.Profiles
{
    public class LoginProfile : Profile 
    { 
       public LoginProfile()
        {
            CreateMap<LoginRequest, LoginEntity>().ReverseMap();
            CreateMap<LoginUpdateRequest, LoginEntity>().ReverseMap();
            CreateMap<LoginEntity, LoginResponse>().ReverseMap();
        }

    }
}
