using System;
using System.Collections.Generic;
using System.Text;
using Wiser.API.Entities.Models;

namespace Wiser.API.Entities.DTO
{
    public class InstituteDTO
    {
        public Guid Id { get; set; }
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
        public string PrincipalMessage { get; set; }
        public string PrincipalPhoto { get; set; }
        public InstituteDTO FetchInstituteDTO(Institute institute)
        {
            this.Id = institute.Id;
            this.Name = institute.Name;
            this.Code = institute.Code;
            this.PhoneNo = institute.PhoneNo;
            this.Email = institute.Email;
            this.Fax = institute.Fax;
            this.LogoPath = institute.LogoPath;
            this.Address = institute.Address;
            this.BankAccountNo = institute.BankAccountNo;
            this.BankIfsc = institute.BankIfsc;
            this.BankName = institute.BankName;
            this.Vision = institute.Vision;
            this.Mission = institute.Mission;
            this.About = institute.About;
            this.PrincipalMessage = institute.PrincipalMessage;
            this.PrincipalPhoto = institute.PrincipalPhoto;

            return this;
        }
        public void MapInstitute(InstituteDTO instituteDto, Institute institute)
        {
            institute.Name = instituteDto.Name;
            institute.Code = instituteDto.Code;
            institute.PhoneNo = instituteDto.PhoneNo;
            institute.Email = instituteDto.Email;
            institute.Fax = instituteDto.Fax;
            institute.LogoPath = instituteDto.LogoPath;
            institute.Address = instituteDto.Address;
            institute.BankAccountNo = instituteDto.BankAccountNo;
            institute.BankIfsc = instituteDto.BankIfsc;
            institute.BankName = instituteDto.BankName;
            institute.Vision = instituteDto.Vision;
            institute.Mission = instituteDto.Mission;
            institute.About = instituteDto.About;
            institute.PrincipalMessage = instituteDto.PrincipalMessage;
            institute.PrincipalPhoto = instituteDto.PrincipalPhoto;
        }
    }
}
