using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.API.BL.Config;
using Wiser.API.BL.Helpers;
using Wiser.API.Entities.DTO;

namespace Wiser.API.BL.I_Services
{
    public interface INewsNotificationService 
    {
        public Task<Response<NewsNotificationDTO>> SaveNewsNotification(NewsNotificationDTO newsNotificationDTO);
        public Task<PagedResponse<List<NewsNotificationDTO>>> GetNewsNotifications(bool IsNotice, Guid? DepartmentId, PaginationFilter filter);
        public Task<Response<bool>> DeleteNewsNotification(Guid Id);
        public Task<Response<bool>> EnableNewsNotification(Guid Id, bool Enabled);

    }
}
