using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.API.Entities.Models
{
    public class Department:BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool Enabled { get; set; }
        public Guid DepartmentCategoryId { get; set; }
        public DepartmentCategory DepartmentCategory { get; set; }
        public List<DepartmentUserMapping> DepartmentUserMappings { get; set; }

        public List<NewsNotification> NewsNotifications { get; set; }
    }
}
