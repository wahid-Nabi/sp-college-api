using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wiser.API.BL.Config;
using Wiser.API.BL.I_Services;
using Wiser.API.Entities.DTO;

namespace Wiser_WEB_API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize(Roles = SystemRoles.Admin)]
    public class InstituteController : ControllerBase
    {
        private readonly IInstituteService instituteService;

        public InstituteController(IInstituteService instituteService)
        {
            this.instituteService = instituteService;
        }
        [HttpPost, Route("save-institute")]
        public async Task<IActionResult> SaveInstitute(InstituteDTO model)
        {
            return Ok(await instituteService.SaveInstituteDetails(model));
        }

        [HttpGet, Route("get-institute")]
        public async Task<IActionResult> GetInstituteDetails()
        {
            return Ok(await instituteService.GetInstituteDetails());
        }


    }
}
