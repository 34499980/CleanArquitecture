using AutoMapper;
using NetCore7.Core.Dtos;
using NetCore7.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(q => q.RolesIds, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.RoleId)))
                .ReverseMap();
        }
    }
}
