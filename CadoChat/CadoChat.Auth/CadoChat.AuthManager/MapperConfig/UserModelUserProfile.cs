using AutoMapper;
using CadoChat.Auth.EF.Entities;
using CadoChat.AuthManager.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.AuthManager.MapperConfig
{
    public class UserModelUserProfile : Profile
    {
        public UserModelUserProfile()
        {
            CreateMap<User, RegisterModel>();
            CreateMap<RegisterModel, User>()
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Username.ToUpper()));
        }
    }
}
