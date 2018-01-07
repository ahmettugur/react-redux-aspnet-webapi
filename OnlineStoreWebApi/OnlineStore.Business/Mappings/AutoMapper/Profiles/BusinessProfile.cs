using AutoMapper;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.Mappings.AutoMapper.Profiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<Product, Product>();
            CreateMap<Category, Category>();
            CreateMap<User, User>();
            CreateMap<Role, Role>();
            CreateMap<UserRole, UserRole>();

        }
    }
}
