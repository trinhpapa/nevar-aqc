using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Core.Models.TestDepartment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.User
{
    public class SYSUserCreateModel : LogEntity<long>
    {
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(100)]
        public string PasswordOrigin { get; set; }

        [StringLength(100)]
        public string PasswordEncrypted { get; set; }

        [StringLength(10)]
        public string PasswordSalt { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Note { get; set; }

        public int RoleId { get; set; }

        public bool ActiveStatus { get; set; }
    }

    public class SYSUserUpdateModel : SYSUserCreateModel
    {
        public long Id { get; set; }

        public string PasswordOld { get; set; }
    }

    public class SYSUserViewModel : ExtensionEntity<long>
    {
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public int RoleId { get; set; }

        [StringLength(100)]
        public string SignalRId { get; set; }

        public bool ActiveStatus { get; set; }

        public CTGDepartmentModel CTGDepartmentEntity { get; set; }

        public CTGRoleModel CTGRoleEntity { get; set; }

        public List<int> FunctionKeys { get; set; }
    }

    public class SYSUserModel : ExtensionEntity<long>
    {
        public List<int> FunctionKeys { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(100)]
        public string PasswordEncrypted { get; set; }

        [StringLength(10)]
        public string PasswordSalt { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public int RoleId { get; set; }

        [StringLength(100)]
        public string SignalRId { get; set; }

        public bool ActiveStatus { get; set; }

        public CTGDepartmentModel CTGDepartmentEntity { get; set; }

        public CTGRoleModel CTGRoleEntity { get; set; }

        //public List<SYSCustomerModel> CRESYSCustomerEntities { get; set; }

        //public List<SYSCustomerModel> MODSYSCustomerEntities { get; set; }

        //public List<SYSCustomerModel> DELSYSCustomerEntities { get; set; }

        //public List<IDCalibrationRequirementModel> CREIDCalibrationRequirementEntities { get; set; }

        //public List<IDCalibrationRequirementModel> MODIDCalibrationRequirementEntities { get; set; }

        //public List<IDCalibrationRequirementModel> DELIDCalibrationRequirementEntities { get; set; }

        //public List<IDTestRequirementModel> CREIDTestRequirementEntities { get; set; }

        //public List<IDTestRequirementModel> MODIDTestRequirementEntities { get; set; }

        //public List<IDTestRequirementModel> DELIDTestRequirementEntities { get; set; }

        //public List<IDTRTestPropertyModel> CREIDTRTestPropertyEntities { get; set; }

        //public List<IDTRTestPropertyModel> MODIDTRTestPropertyEntities { get; set; }

        //public List<IDTRTestPropertyModel> DELIDTRTestPropertyEntities { get; set; }

        //public List<IDTRImplementerModel> IDTRImplementerEntities { get; set; }

        //public List<IDTRImplementerModel> CREIDTRImplementerEntities { get; set; }

        //public List<IDTRImplementerModel> MODIDTRImplementerEntities { get; set; }

        //public List<IDTRImplementerModel> DELIDTRImplementerEntities { get; set; }

        //public List<SYSRequirementInvoiceModel> CRESYSRequirementInvoiceEntities { get; set; }

        //public List<SYSRequirementInvoiceModel> MODSYSRequirementInvoiceEntities { get; set; }

        //public List<SYSRequirementInvoiceModel> DELSYSRequirementInvoiceEntities { get; set; }

        //public List<SYSRoleFunctionModel> CRESYSRoleFunctionEntities { get; set; }

        //public List<SYSRoleFunctionModel> MODSYSRoleFunctionEntities { get; set; }

        //public List<SYSRoleFunctionModel> DELSYSRoleFunctionEntities { get; set; }
    }
}