using System;
using System.Collections.Generic;
using System.Text;

namespace Wiser.API.Entities.Models
{
    public class NewsNotification : BaseModel
    {
        public bool IsNotice { get; set; } // true for Notification false for News
        public string NewsImagePath { get; set; }
        public string Title { get; set; }
        public string FileLink { get; set; }
        public bool HasContent { get; set; } = false;
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public bool Enabled { get; set; } = true;
        public string Slug { get; set; }
        public Guid? DepartmentId { get; set; }

        public Department Department { get;set; }

    }
}
