using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wiser.API.BL.Config;
using Wiser.API.Entities.DTO;
using Wiser.API.Entities.Models;

namespace Wiser.API.BL.I_Services
{
    public interface ISystemUserAuthenticationService
    {
        Task<Response<IdentityResult>> RegisterSystemUser(UserProfileDTO systemUser);
        Task<Response<LoginResponseDTO>> LoginSystemUser(LoginModelDTO model);
        Task<Response<string>> CreateRole(string RoleName);
        Task<Response<LoginResponseDTO>> IsUserLoggedIn();
    }
}
