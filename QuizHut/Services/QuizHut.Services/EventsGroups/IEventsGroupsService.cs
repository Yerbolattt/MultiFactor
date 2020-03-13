﻿namespace QuizHut.Services.EventsGroups
{
    using System.Threading.Tasks;

    public interface IEventsGroupsService
    {
        Task CreateAsync(string eventId, string groupId);

        Task DeleteAsync(string eventId, string groupId);

        Task<string[]> GetAllGroupsIdsByEventIdAsync(string eventId);
    }
}