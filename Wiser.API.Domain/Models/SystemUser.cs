using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wiser.API.Entities.Models
{
    public class SystemUser : IdentityUser
    {
        public bool IsDeleted { get; set; }
        public string Name { get; set; }

        public List<DepartmentUserMapping> DepartmentUserMappings { get; set; }
    }

    public class SystemRole : IdentityRole
    {
        public bool IsDeleted { get; set; } = false;
    }

    public class SystemUserRole : IdentityUserRole<string>
    {
        public bool IsDeleted { get; set; } = false;
    }
}
