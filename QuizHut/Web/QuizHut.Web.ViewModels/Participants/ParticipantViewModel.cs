﻿namespace QuizHut.Web.ViewModels.Participants
{
    using AutoMapper;
    using QuizHut.Data.Models;
    using QuizHut.Services.Mapping;

    public class ParticipantViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool IsAssigned { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, ParticipantViewModel>()
               .ForMember(
                   x => x.FullName,
                   opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"));
        }
    }
}
