using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wiser.API.BL.Helpers;
using Wiser.API.BL.I_Services;
using Wiser.API.Entities.DTO;

namespace Wiser_WEB_API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class NewsNotificationController : ControllerBase
    {
        private readonly INewsNotificationService newsNotificationService;

        public NewsNotificationController(INewsNotificationService newsNotificationService)
        {
            this.newsNotificationService = newsNotificationService;
        }

        [HttpPost, Route("save-news-notification")]
        public async Task<IActionResult> SaveInstitute(NewsNotificationDTO model)
        {
            return Ok(await newsNotificationService.SaveNewsNotification(model));
        }

        [HttpGet, Route("get-news-notifications")]
        public async Task<IActionResult> GetNewsNotifications(bool IsNotice, Guid? DepartmentId = null, [FromQuery] PaginationFilter filter = null)
        {
            return Ok(await newsNotificationService.GetNewsNotifications(IsNotice, DepartmentId,filter));
        }

        [HttpPost, Route("delete-news-notification")]
        public async Task<IActionResult> DeleteNewsNotification(Guid Id)
        {
            return Ok(await newsNotificationService.DeleteNewsNotification(Id));
        }
        
        [HttpPost, Route("enable-news-notification")]
        public async Task<IActionResult> EnableNewsNotification(Guid Id, bool Enabled)
        {
            return Ok(await newsNotificationService.EnableNewsNotification(Id, Enabled));
        }
    }
}
