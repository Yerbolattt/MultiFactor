﻿namespace QuizHut.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizHut.Common;
    using QuizHut.Data.Models;
    using QuizHut.Services.Events;
    using QuizHut.Web.Infrastructure.Filters;
    using QuizHut.Web.ViewModels.Events;

    public class ResultsController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEventsService eventService;

        public ResultsController(UserManager<ApplicationUser> userManager, IEventsService eventService)
        {
            this.userManager = userManager;
            this.eventService = eventService;
        }

        [ClearDashboardRequestInSessionActionFilterAttribute]
        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> EventResultsDetails(string id)
        {
            var eventModel = await this.eventService.GetEventModelByIdAsync<EventWithGroupAndQuizNamesViewModel>(id);
            if (this.HttpContext.Session.GetString(GlobalConstants.DashboardRequest) != null)
            {
                this.ViewData[GlobalConstants.DashboardRequest] = true;
            }

            return this.View(eventModel);
        }
    }
}
