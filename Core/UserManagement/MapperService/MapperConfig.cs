using AutoMapper;
using Core.UserManagement.Entities;
using Core.UserManagement.Models;
using Core.UserManagement.Abstractions.Dtos;

namespace Core.UserManagement.MapperServices
{
    internal class MapperConfig
    {
        #region User Mapper Config
        public class UserMapperConfig : Profile
        {
            public UserMapperConfig()
            {
                CreateMap<UserEntity, UserManagementModel>();
                CreateMap<UserEntity, UserGetDto>();

                CreateMap<UserRegisterDto, UserEntity>()
                    .ForMember(des => des.Otp, opt => opt.MapFrom(src => ""))
                    .ForMember(des => des.Sex, opt => opt.MapFrom(src => false))
                    .ForMember(des => des.IsMarried, opt => opt.MapFrom(src => false))
                    .ForMember(des => des.Age, opt => opt.MapFrom(src => 0))
                    .ForMember(des => des.Education, opt => opt.MapFrom(src => ""))
                    .ForMember(des => des.Work, opt => opt.MapFrom(src => ""))
                    .ForMember(des => des.LocationOfLiving, opt => opt.MapFrom(src => ""));
            }
        }

        #endregion Private Helper

    }
}