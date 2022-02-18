using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiser.API.BL.Config;
using Wiser.API.Entities.DTO;

namespace Wiser.API.BL.I_Services {
    public interface IDepartmentService {
        Task<Response<string>> SaveDepartmentCategory(DepartmentCategoryDTO model);
        Task<Response<List<DepartmentDetailDTO>>> GetDepartmentDetails(Guid categoryId);
        Task<Response<string>> SaveDepartment(DepartmentDTO model);
        Task<Response<string>> DeleteDepartmentCategory(Guid Id);
        Task<Response<string>> DeleteDepartment(Guid Id);
        Task<Response<DepartmentDTO>> GetDepartmentById(Guid Id);
        Task<Response<List<DepartmentSectionDTO>>> GetDepartmentSections();
    }
}
