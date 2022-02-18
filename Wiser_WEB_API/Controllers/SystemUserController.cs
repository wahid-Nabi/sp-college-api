using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wiser.API.BL.Config;
using Wiser.API.BL.I_Services;
using Wiser.API.Entities.DTO;
using Wiser.API.Entities.Models;

namespace Wiser_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUserController : ControllerBase
    {
        public readonly ISystemUserAuthenticationService service;

        public SystemUserController(ISystemUserAuthenticationService service)
        {
            this.service = service;
        }

        [Authorize(Roles = SystemRoles.SuperAdmin)]
        [HttpPost, Route("/api/v1/system-register")]
        public async Task<IActionResult> RegisterSystemUser(UserProfileDTO user)
        {
            var response = await this.service.RegisterSystemUser(user);
            return Ok(response);
        }

        [HttpPost, Route("/api/v1/system-login")]
        public async Task<IActionResult> LoginSystemUser(LoginModelDTO model)
        {
            var response = await this.service.LoginSystemUser(model);
            return Ok(response);
        }

       // [Authorize(Roles = SystemRoles.SuperAdmin)]
        [HttpPost, Route("/api/v1/create-role")]
        public async Task<IActionResult> CreateRole(string RoleName)
        {
            var response = await this.service.CreateRole(RoleName);
            return Ok(response);
        }

        [Authorize]
        [HttpGet, Route("/api/v1/is-user-authenticated")]
        public async Task<IActionResult> IsUserLoggedIn()
        {
            var response = await this.service.IsUserLoggedIn();
            return Ok(response);
        }
    }
}
