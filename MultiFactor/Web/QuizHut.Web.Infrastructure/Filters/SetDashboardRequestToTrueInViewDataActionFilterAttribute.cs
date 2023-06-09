﻿namespace MultiFactor.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using MultiFactor.Common;

    public class SetDashboardRequestToTrueInViewDataActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;

            if (controller.HttpContext.Session.GetString(GlobalConstants.DashboardRequest) != null)
            {
                controller.ViewData[GlobalConstants.DashboardRequest] = true;
            }
        }
    }
}
