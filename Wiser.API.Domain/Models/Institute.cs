using System;
using System.Collections.Generic;
using System.Text;

namespace Wiser.API.Entities.Models
{
    public class Institute : BaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string LogoPath { get; set; }
        public string Address { get; set; }
        public string BankAccountNo { get; set; }
        public string BankIfsc { get; set; }
        public string BankName { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }
        public string About { get; set; }
        public string PrincipalPhoto { get; set; }
        public string PrincipalMessage { get; set; }
    }
}
