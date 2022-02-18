using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wiser.API.BL.Config;
using Wiser.API.Entities.DTO;

namespace Wiser.API.BL.I_Services
{
    public interface IInstituteService
    {
        Task<Response<string>> SaveInstituteDetails(InstituteDTO model);
        Task<Response<InstituteDTO>> GetInstituteDetails();
    }
}
