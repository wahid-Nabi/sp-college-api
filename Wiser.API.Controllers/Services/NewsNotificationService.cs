using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.API.BL.Config;
using Wiser.API.BL.Helpers;
using Wiser.API.BL.I_Services;
using Wiser.API.Entities;
using Wiser.API.Entities.DTO;
using Wiser.API.Entities.Models;

namespace Wiser.API.BL.Services
{
    public class NewsNotificationService : INewsNotificationService
    {
        private readonly WiserContext wiserContext;
        private readonly Global global;
        private readonly ILogger<string> logger;

        public NewsNotificationService(WiserContext wiserContext, Global global, ILogger<string> _logger)
        {
            this.wiserContext = wiserContext;
            this.global = global;
            logger = _logger;
        }
        public async Task<Response<bool>> DeleteNewsNotification(Guid Id)
        {
            var item = await this.wiserContext.NewsNotifications.FirstOrDefaultAsync(x => x.Id == Id);
            if (item != null)
            {
                this.wiserContext.NewsNotifications.Remove(item);
                await this.wiserContext.SaveChangesAsync();
                logger.LogInformation($"Executed NewsNotificationService/DeleteNewsNotification:Deleted successfully");
                return new Response<bool>() { Message = "Deleted successfully", Data = true };
            }
            return new Response<bool>() { Message = "Record not found", Data = false };
        }
        public async Task<Response<bool>> EnableNewsNotification(Guid Id, bool Enabled)
        {
            var item = await this.wiserContext.NewsNotifications.FirstOrDefaultAsync(x => x.Id == Id);
            if (item != null)
            {
                item.Enabled = Enabled;
                item.ModifiedBy = global.GetCurrentUserId();
                item.ModifiedDate = Constants.WISER_TIME;
                await this.wiserContext.SaveChangesAsync();
                logger.LogInformation($"Executed NewsNotificationService/EnableNewsNotification:Status updated successfully");
                return new Response<bool>() { Message = "Status updated successfully", Data = true };
            }
            return new Response<bool>() { Message = "Record not found", Data = false };
        }
        public async Task<PagedResponse<List<NewsNotificationDTO>>> GetNewsNotifications(bool IsNotice, Guid? DepartmentId, PaginationFilter filter)
        {
            var response = new PagedResponse<List<NewsNotificationDTO>>(null, 0, 0);
            var data = await this.wiserContext.NewsNotifications
                                        .AsNoTracking()
                                        .Where(x => x.IsNotice == IsNotice)
                                        .Where(x => DepartmentId.HasValue ? x.DepartmentId == DepartmentId : true)
                                        .OrderByDescending(x => x.CreatedDate)
                                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                                        .Take(filter.PageSize)
                                        .ToListAsync();
            var count = await this.wiserContext.NewsNotifications
                                        .AsNoTracking()
                                        .Where(x => x.IsNotice == IsNotice)
                                        .Where(x => DepartmentId.HasValue ? x.DepartmentId == DepartmentId : true).CountAsync();
            if (data.Any())
            {
                var dataToSend = data.Select(x => new NewsNotificationDTO()
                {
                    Id = x.Id,
                    NewsImagePath = x.NewsImagePath,
                    Title = x.Title,
                    FileLink = x.FileLink,
                    HasContent = x.HasContent,
                    Content = x.Content,
                    Slug = x.Slug,
                    Enabled = x.Enabled,
                    PublishDate = x.PublishDate,
                    CreatedDate = x.CreatedDate,
                    DepartmentId = x.DepartmentId
                }).ToList();
                


                response = PaginationHelper.CreatePagedReponse<List<NewsNotificationDTO>>(dataToSend, filter, count);
                if (response.Data.Any())
                {
                    response.Message = "Data found";
                    response.Count = response.Data.Count;
                    logger.LogInformation($"Executed NewsNotificationService/GetNewsNotifications:Data found");
                }
                else
                    response.Message = "No Record found";

            }
            response.Message = "No Record found";

            return response;
        }
        public async Task<Response<NewsNotificationDTO>> SaveNewsNotification(NewsNotificationDTO newsNotificationDTO)
        {
            var response = new Response<NewsNotificationDTO>();
            var item = await this.wiserContext.NewsNotifications.FirstOrDefaultAsync(x => x.Id == newsNotificationDTO.Id);
            Guid Id;
            if (item != null)
            {
                MapNewsNotificationToDTO(newsNotificationDTO, item);
                response.Message = "Item updated successfully";
                logger.LogInformation($"Executed NewsNotificationService/SaveNewsNotification:{response.Message}");
                Id = item.Id;
            }
            else
            {
                var newItem = new NewsNotification();
                MapNewsNotificationToDTO(newsNotificationDTO, newItem);
                await this.wiserContext.AddAsync(newItem);
                response.Message = "Item created successully";
                logger.LogInformation($"Executed NewsNotificationService/SaveNewsNotification:{response.Message}");
                Id = newItem.Id;
            }
            await this.wiserContext.SaveChangesAsync();
            newsNotificationDTO.Id = Id;
            response.Data = newsNotificationDTO;
            return response;
        }

        #region private region
        public void MapNewsNotificationToDTO(NewsNotificationDTO source, NewsNotification destination)
        {
            destination.NewsImagePath = source.NewsImagePath;
            destination.Title = source.Title;
            destination.FileLink = source.FileLink;
            destination.HasContent = source.HasContent;
            destination.Content = source.Content;
            destination.Enabled = source.Enabled;
            destination.PublishDate = source.PublishDate;
            destination.DepartmentId = source.DepartmentId;

            if (source.Id.Equals(Constants.DEFAULT_GUID))
            {
                destination.CreatedBy = global.GetCurrentUserId();
                destination.CreatedDate = Constants.WISER_TIME;
                destination.Slug = source.Title.Replace(" ", "-");
            }
            else
            {
                destination.ModifiedBy = global.GetCurrentUserId();
                destination.ModifiedDate = Constants.WISER_TIME;
            }

        }

        
        #endregion
    }
}
