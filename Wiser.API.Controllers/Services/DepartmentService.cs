using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wiser.API.BL.Config;
using Wiser.API.BL.I_Services;
using Wiser.API.Entities;
using Wiser.API.Entities.DTO;
using Wiser.API.Entities.Models;

namespace Wiser.API.BL.Services {
    public class DepartmentService : IDepartmentService {
        private WiserContext _wiserContext;
        public DepartmentService(WiserContext wiserContext) {
            _wiserContext = wiserContext;
        }

        /// <summary>
        /// Get all department details 
        /// </summary>
        /// <param name="categoryId">department category id: if this is null then it will fetch all the departments.</param>
        /// <returns></returns>
        public async Task<Response<List<DepartmentDetailDTO>>> GetDepartmentDetails(Guid categoryId) {
            Response<List<DepartmentDetailDTO>> response = new Response<List<DepartmentDetailDTO>>();
            List<DepartmentDetailDTO> departmentDetailList = new List<DepartmentDetailDTO>();
            if (categoryId == Constants.DEFAULT_GUID) {
                var allDepartments = await _wiserContext.DepartmentCategory
                                            .Include(x => x.Departments)
                                            .Where(x => x.IsDeleted == false).ToListAsync();
                if (allDepartments.Count > 0) {
                    MapDepartmentDetails(allDepartments, departmentDetailList);
                    response.Data = departmentDetailList;
                    response.Message = "Department categories found";
                    response.Count = departmentDetailList.Count;
                } else {
                    response.Message = "No department categories found";
                }
            } else {
                var departmentById = await _wiserContext.DepartmentCategory
                                            .Include(x => x.Departments)
                                            .Where(x => x.IsDeleted == false && x.Id == categoryId).FirstOrDefaultAsync();
                if (departmentById != null) { 
                    List<DepartmentCategory> allDepartments = new List<DepartmentCategory> { departmentById };
                    MapDepartmentDetails(allDepartments, departmentDetailList);
                    response.Data = departmentDetailList;
                    response.Message = "Department category found";
                    response.Count = departmentDetailList.Count;
                } else {
                    response.Message = "Department category details not found";
                }
            }
            return response;
        }

        /// <summary>
        /// Save and update Department Category
        /// </summary>
        /// <param name="model">department category data to be saved or updated</param>
        /// <returns></returns>
        public async Task<Response<string>> SaveDepartmentCategory(DepartmentCategoryDTO model) {
            Response<string> response = new Response<string>();
            if (model != null) {
                var isAlreadyExist = await _wiserContext.DepartmentCategory
                                                .Where(x => x.Name == model.Name && x.Id != model.Id && x.IsDeleted == false).AnyAsync();
                if (!isAlreadyExist) {
                    var existingDepartmentCategory = await _wiserContext.DepartmentCategory
                                                        .Where(x => x.Id == model.Id && x.IsDeleted == false).FirstOrDefaultAsync();
                    if (existingDepartmentCategory != null) {
                        existingDepartmentCategory.Name = model.Name;
                        response.Message = "Department Updated Successfully";
                    } else {
                        DepartmentCategory newDepartmentCategory = new DepartmentCategory() {
                            Name = model.Name,
                        };
                        await _wiserContext.DepartmentCategory.AddAsync(newDepartmentCategory);

                        response.Message = "Department added successfully";
                    }
                    await _wiserContext.SaveChangesAsync();
                } else {
                    response.Message = "Department category already exists";
                }
            } else {
                response.Message = "Payload is empty";
            }
            return response;
        }

        /// <summary>
        /// Save or update department
        /// </summary>
        /// <param name="model">Department data to be saved or updated</param>
        /// <returns></returns>
        public async Task<Response<string>> SaveDepartment(DepartmentDTO model) {
            Response<string> response = new Response<string>();
            if (model != null) {
                var isAlreadyExist = await _wiserContext.Department
                                                   .Where(x => x.Name == model.Name && x.Id != model.Id && x.IsDeleted == false).AnyAsync();
                if (!isAlreadyExist) {
                    var existingDepartment = await _wiserContext.Department
                                                    .Where(x => x.Id == model.Id && x.IsDeleted == false).FirstOrDefaultAsync();
                    if (existingDepartment != null) {
                        existingDepartment.Name = model.Name;
                        response.Message = "Department Updated Successfully";
                    } else {
                        Department newDepartment = new Department() {
                            Name = model.Name,
                            DepartmentCategoryId = model.Id,
                        };
                        await _wiserContext.Department.AddAsync(newDepartment);
                        response.Message = "Department added successfully";
                        response.Data = null;
                    }
                    await _wiserContext.SaveChangesAsync();
                } else {
                    response.Message = "Department name already exists";
                }
            } else {
                response.Message = "Payload is empty";
            }
            return response;
        }

        /// <summary>
        /// Delete Department Category
        /// </summary>
        /// <param name="Id">department category id to be deleted</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<string>> DeleteDepartmentCategory(Guid Id) {
            Response<string> response = new Response<string>();
            if (Id == default(Guid)) {
                var deptCategory = await _wiserContext.DepartmentCategory
                                    .Where(x => x.Id == Id && x.IsDeleted == false).FirstOrDefaultAsync();
                if (deptCategory != null) {
                    var departments = await _wiserContext.Department
                                        .Where(x => x.DepartmentCategoryId == deptCategory.Id && !x.IsDeleted).ToListAsync();
                    if (departments.Any()) {
                        foreach (var department in departments) {
                            _wiserContext.Department.Remove(department);
                        }
                    }
                    _wiserContext.DepartmentCategory.Remove(deptCategory);
                    response.Message = "Department Category deleted successfully"; 
                } else {
                    response.Message = "Department Category not found";
                }
            } else {
                response.Message = "Id is not valid";
            }
            return response;
        }

        /// <summary>
        /// Delete Department
        /// </summary>
        /// <param name="Id">department id to be deleted</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<string>> DeleteDepartment(Guid Id) {
            Response<string> response = new Response<string>();
            if (Id != default(Guid)) {
                var department = await _wiserContext.Department
                                    .Where(x => x.Id == Id && x.IsDeleted == false).FirstOrDefaultAsync();
                if (department != null) {
                    _wiserContext.Department.Remove(department);
                    await _wiserContext.SaveChangesAsync();
                    response.Message = "Department deleted successfully";
                } else {
                    response.Message = "Department not found";
                }
            } else {
                response.Message = "Id is not valid";
            }
            return response;
        }

        /// <summary>
        /// Fetch single Department based on id
        /// </summary>
        /// <param name="Id">department id to be fetched</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<DepartmentDTO>> GetDepartmentById(Guid Id) {
            Response<DepartmentDTO> response = new Response<DepartmentDTO>();
            if (Id != default(Guid)) {
                var department = await _wiserContext.Department
                                    .Where(x => x.Id == Id && x.IsDeleted == false).FirstOrDefaultAsync();
                if (department != null) {
                    response.Data = new DepartmentDTO {
                        Id = department.Id,
                        Name = department.Name,
                        Slug = department.Slug,
                        DepartmentCategoryId = department.DepartmentCategoryId
                    };
                    response.Message = "Department found successfully";
                } else {
                    response.Message = "Department not found";
                }
            } else {
                response.Message = "Id is not valid";
            }
            return response;
        }

        //Get All Department Sections
        public async Task<Response<List<DepartmentSectionDTO>>> GetDepartmentSections()
        {
            var response = new Response<List<DepartmentSectionDTO>>();

            var departmentSectionsDTO = new List<DepartmentSectionDTO>();

            var existingDeptSection = await _wiserContext.DeparmentSection.Where(x => x.IsDeleted == false).ToListAsync();
            if (existingDeptSection.Count() != 0)
            {
                foreach (var deptSection in existingDeptSection)
                {
                    var DeptSectionDTO = new DepartmentSectionDTO()
                    {
                        Id = deptSection.Id,
                        Name = deptSection.Name
                    };

                    departmentSectionsDTO.Add(DeptSectionDTO);
                }

                response.Data = departmentSectionsDTO;
                response.Message = "Department Section Found";

            }
            else
            {
                response.Message = "Department Sections Not Found";
            }
            return response;
        }

        #region MappingMethods
        public void MapDepartmentDetails(List<DepartmentCategory> source, List<DepartmentDetailDTO> destination) {
            foreach (var departmentCategory in source) {
                DepartmentDetailDTO departmentDetailDTO = new DepartmentDetailDTO() {
                    Id = departmentCategory.Id,
                    Name = departmentCategory.Name,
                };
                foreach (var department in departmentCategory.Departments) {
                    DepartmentDTO departmentDTO = new DepartmentDTO() {
                        Id = department.Id,
                        Name = department.Name,
                        Slug = department.Slug,
                    };
                    departmentDetailDTO.Departments.Add(departmentDTO);
                }

                destination.Add(departmentDetailDTO);
            }
        }
        #endregion
    }
}
