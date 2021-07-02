using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Core.Models.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class SYSCustomerCreateModel : LogEntity<long>
    {
        [StringLength(500)]
        public string Name { get; set; }

        public int? CustomerTypeId { get; set; }

        public int? ProvinceId { get; set; }

        public int? DistrictId { get; set; }

        public int? WardsId { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        public SYSUserModel CRESYSUserEntity { get; set; }
        public SYSUserModel MODSYSUserEntity { get; set; }
        public SYSUserModel DELSYSUserEntity { get; set; }
        public CTGCustomerTypeModel CTGCustomerTypeEntity { get; set; }
    }

    public class SYSCustomerCreateResultModel
    {
        public long Id { get; set; }
    }

    public class SYSCustomerUpdateModel : SYSCustomerCreateModel
    {
        public long Id { get; set; }
    }

    public class SYSCustomerViewModel : SYSCustomerUpdateModel
    {
    }

    public class SYSCustomerModel : ExtensionEntity<long>
    {
        [StringLength(500)]
        public string Name { get; set; }

        public int? CustomerTypeId { get; set; }

        public int? ProvinceId { get; set; }

        public int? DistrictId { get; set; }

        public int? WardsId { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        public SYSUserModel CRESYSUserEntity { get; set; }
        public SYSUserModel MODSYSUserEntity { get; set; }
        public SYSUserModel DELSYSUserEntity { get; set; }
        public CTGCustomerTypeModel CTGCustomerTypeEntity { get; set; }

        public List<SYSRequirementInvoiceModel> SYSRequirementInvoiceEntities { get; set; }
    }
}