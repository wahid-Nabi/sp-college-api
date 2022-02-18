using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    public class InstituteService : IInstituteService
    {
        private readonly WiserContext wiserContext;
        private readonly Global global;
        private readonly ILogger<string> logger;

        public InstituteService(WiserContext wiserContext, Global global, ILogger<string> _logger)
        {
            this.wiserContext = wiserContext;
            this.global = global;
            logger = _logger;
        }

        public async Task<Response<InstituteDTO>> GetInstituteDetails()
        {
            var response = new Response<InstituteDTO>();
            var instituteDetails = await wiserContext.Institutes.FirstOrDefaultAsync();
            if (instituteDetails != null)
            {
                response.Data = new InstituteDTO().FetchInstituteDTO(instituteDetails);
                response.Count = 1;
                response.Message = "Institute details found";
            }
            else
            {

                response.Message = "Institute details not found";
                
            }
            logger.LogInformation($"Executed InstituteService/GetInstituteDetails:{response.Message}");
            return response;
        }

        public async Task<Response<string>> SaveInstituteDetails(InstituteDTO model)
        {
            var response = new Response<string>()
            {
                Success = false,
                Data = null,
                Message = Constants.ERROR_OCCURRED
            };
            if (model != null)
            {
                if (model.Id == Constants.DEFAULT_GUID) //create
                {
                    Institute institute = new Institute();
                    model.MapInstitute(model, institute);
                    institute.CreatedBy = global.GetCurrentUserId();
                    await wiserContext.Institutes.AddAsync(institute);
                    response.Message = "Institute details created successfully";
                    response.Success = true;
                }
                else
                {
                    var instituteExisting = await wiserContext.Institutes.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (instituteExisting != null) // update
                    {
                        model.MapInstitute(model, instituteExisting);
                        instituteExisting.ModifiedBy = global.GetCurrentUserId();
                        instituteExisting.ModifiedDate = Constants.WISER_TIME;
                        response.Message = "Institute details updated successfully";
                        response.Success = true;
                    }
                    else
                        response.Errors = new List<string>() { "You are trying to update the invalid institute" };

                }
                await wiserContext.SaveChangesAsync();
            }
            else
                response.Errors = new List<string>() { Constants.PAYLOAD_EMPTY };
            if (response.Success)
                logger.LogInformation($"{response.Message}");
            else
                logger.LogError($"{response.Message}");
            return response;
        }

    }

}
