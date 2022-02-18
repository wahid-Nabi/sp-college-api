using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Wiser.API.BL.Config;
using Wiser.API.BL.I_Services;
using Wiser.API.Entities.DTO;

namespace Wiser_WEB_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase {
        private IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService) { 
            _departmentService = departmentService;
        }

        [Authorize(Roles = SystemRoles.Admin)]
        [HttpPost, Route("save-department-category")]
        public async Task<IActionResult> SaveDepartmentCategory(DepartmentCategoryDTO model) => 
                    Ok(await _departmentService.SaveDepartmentCategory(model));

        [Authorize(Roles = SystemRoles.Admin)]
        [HttpGet, Route("get-all-departments")]
        public async Task<IActionResult> GetAllDepartments(Guid categoryId = default(Guid)) => 
                    Ok(await _departmentService.GetDepartmentDetails(categoryId));

        [Authorize(Roles = SystemRoles.Admin)]
        [HttpPost, Route("save-department")]
        public async Task<IActionResult> SaveDepartment(DepartmentDTO model) => 
                    Ok(await _departmentService.SaveDepartment(model));

        [Authorize(Roles = SystemRoles.Admin)]
        [HttpPost, Route("delete-department-category")]
        public async Task<IActionResult> DeleteDepartmentCategory(Guid Id) => 
                    Ok(await _departmentService.DeleteDepartmentCategory(Id));

        [Authorize(Roles = SystemRoles.Admin)]
        [HttpPost, Route("delete-department")]
        public async Task<IActionResult> DeleteDepartment(Guid Id) =>
                    Ok(await _departmentService.DeleteDepartment(Id));

        [Authorize(Roles = SystemRoles.Admin)]
        [HttpGet, Route("get-department")]
        public async Task<IActionResult> GetDepartmentById(Guid Id = default(Guid)) =>
                    Ok(await _departmentService.GetDepartmentById(Id));

        [Authorize(Roles = SystemRoles.Admin)]
        [HttpGet, Route("get-department-sections")]
        public async Task<IActionResult> GetDepartmentSections() =>
                    Ok(await _departmentService.GetDepartmentSections());
    }
}
