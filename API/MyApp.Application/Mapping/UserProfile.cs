using AutoMapper;
using MyApp.Application.Response;
using MyApp.Business_Core_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<EmployeeEntity, GetUserResponse>()
                .ForMember(dest => dest.FullName,
                    src => src.MapFrom(x => x.FIrstName + " " + x.LastName))
                .ForMember(dest => dest.Email,
                    src => src.MapFrom(x => x.Email))
                .ForMember(dest => dest.Phone,
                    src => src.MapFrom(x => x.Phone));
        }
    }
}
