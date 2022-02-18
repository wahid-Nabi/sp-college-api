using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.API.Entities.Models
{
    public class DepartmentSection : BaseModel
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int SequenceNumber { get; set; }
    }
}
