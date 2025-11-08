using AutoMapper;
using E_Club.Application.DTOs.Auth.Response;
using E_Club.Application.DTOs.Clubs.Request;
using E_Club.Application.DTOs.Clubs.Response;
using E_Club.Application.DTOs.User.Response;
using E_Club.Application.DTOs.Users.Request;
using DomainModels.Entities;
using E_Club.Application.DTOs.UserTypes.Response;

namespace E_Club.Application.Mappings
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() 
        {
            // Club
            CreateMap<Club, ClubDtoResponse>();
            CreateMap<ClubDtoCreateRequest, Club>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            
            // Auth
            CreateMap<User, LoginDtoResponse>();
            CreateMap<User, AuthMeDtoResponse>();

            // UserType
            CreateMap<UserType, UserTypeDtoResponse>();

            // User
            CreateMap<User, UserDtoResponse>();
            CreateMap<UserDtoCreateRequest, User>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}