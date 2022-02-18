using System;
using System.Collections.Generic;

namespace Wiser.API.Entities.DTO {
    public class DepartmentDTO {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public Guid DepartmentCategoryId { get; set; }
    }

    public class DepartmentCategoryDTO {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentDetailDTO {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<DepartmentDTO> Departments { get; set; }
    }
}
