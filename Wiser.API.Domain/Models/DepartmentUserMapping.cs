using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.API.Entities.Models
{
    public class DepartmentUserMapping:BaseModel
    {
        public Guid DepartmentId { get; set; }
        public string UserId { get; set; }
        public bool Enabled { get; set; }
        public Department Department { get; set; }
        public SystemUser SystemUser { get; set; }
    }
}
