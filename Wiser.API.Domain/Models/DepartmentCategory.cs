using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.API.Entities.Models
{
    public class DepartmentCategory : BaseModel
    {
        public string Name { get; set; }
        public List<Department> Departments { get; set; }
    }
}
