﻿namespace MultiFactor.Web.ViewModels.UsersInRole
{
    using AutoMapper;
    using MultiFactor.Data.Models;
    using MultiFactor.Services.Mapping;

    public class UserInRoleViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserInRoleViewModel>()
               .ForMember(
                   x => x.FullName,
                   opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"));
        }
    }
}
