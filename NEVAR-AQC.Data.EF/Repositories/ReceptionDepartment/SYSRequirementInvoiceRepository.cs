// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data.EF </Project>
//     <File>
//         <Name> SYSRequirementInvoiceRepository.cs </Name>
//         <Created> 26/6/2019 - 23:53 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.ReceptionDepartment;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NEVAR_AQC.Data.EF.Repositories.ReceptionDepartment
{
    public class SYSRequirementInvoiceRepository : RepositoryBase<SYSRequirementInvoiceEntity, NEVARDbContext>, ISYSRequirementInvoiceRepository
    {
        private string _connectionString;
        private NEVARDbContext _context;

        public SYSRequirementInvoiceRepository(NEVARDbContext context,
            IConfiguration configuration) : base(context)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("NEVARConnection");
        }

        public IEnumerable<SYSRequirementInvoiceEntity> GetByUser(long userId)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                return conn.GetList<SYSRequirementInvoiceEntity>(new { CreatedBy = userId });
            }
        }

        public IEnumerable<SYSRequirementInvoiceEntity> GetPagedByUser(long userId, int pageIndex, int pageSize)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                return conn.GetListPaged<SYSRequirementInvoiceEntity>(pageIndex,
                    pageSize,
                    "WHERE CreatedBy = @CreatedBy",
                    "CreatedTime DESC",
                    new { CreatedBy = userId });
            }
        }

        public IQueryable<SYSRequirementInvoiceEntity> TestRequirementReport(string invoiceNo, int edition)
        {
            return _context.SYSRequirementInvoice
                .Where(w => w.InvoiceNo == invoiceNo && w.Edition == edition && w.IsDeleted == false)
                .Select(item => new SYSRequirementInvoiceEntity
                {
                    Id = item.Id,
                    RequirementTypeId = item.RequirementTypeId,
                    Serial = item.Serial,
                    SerialYear = item.SerialYear,
                    InvoiceNo = item.InvoiceNo,
                    Edition = item.Edition,
                    FieldId = item.FieldId,
                    CustomerId = item.CustomerId,
                    Representative = item.Representative,
                    RepresentativePhone = item.RepresentativePhone,
                    OtherInformation = item.OtherInformation,
                    IsSaveSpecimen = item.IsSaveSpecimen,
                    SaveSpecimenTime = item.SaveSpecimenTime,
                    IsUseSubcontractors = item.IsUseSubcontractors,
                    ResultDay = item.ResultDay,
                    CreatedTime = item.CreatedTime,
                    CreatedBy = item.CreatedBy,
                    ReturnInvoiceResultTypeId = item.ReturnInvoiceResultTypeId,
                    ResultInvoiceAmount = item.ResultInvoiceAmount,
                    ProcessStatusId = item.ProcessStatusId,
                    SYSCustomerEntity = item.SYSCustomerEntity,
                    CTGReturnInvoiceResultTypeEntity = item.CTGReturnInvoiceResultTypeEntity,
                    CRESYSUserEntity = item.CRESYSUserEntity,
                    IDTestRequirementEntities = item.IDTestRequirementEntities.Select(re => new IDTestRequirementEntity
                    {
                        Id = re.Id,
                        RequirementInvoiceId = re.RequirementInvoiceId,
                        ObjectId = re.ObjectId,
                        InvoiceResultSerial = re.InvoiceResultSerial,
                        InvoiceResultYear = re.InvoiceResultYear,
                        InvoiceResultNo = re.InvoiceResultNo,
                        InvoiceResultDate = re.InvoiceResultDate,
                        SpecimenName = re.SpecimenName,
                        SpecimenSymbol = re.SpecimenSymbol,
                        SpecimenOrder = re.SpecimenOrder,
                        SpecimenCode = re.SpecimenCode,
                        ImageLink = re.ImageLink,
                        SpecimenAmount = re.SpecimenAmount,
                        SpecimenStatus = re.SpecimenStatus,
                        SpecimenQuantum = re.SpecimenQuantum,
                        CTGTestObjectEntity = re.CTGTestObjectEntity,
                        IDTRTestPropertyEntities = re.IDTRTestPropertyEntities.Select(pr => new IDTRTestPropertyEntity
                        {
                            Id = pr.Id,
                            SpecimenId = pr.SpecimenId,
                            TestPropertyId = pr.TestPropertyId,
                            TestMethodId = pr.TestMethodId,
                            OrderNumber = pr.OrderNumber,
                            PlanFromTime = pr.PlanFromTime,
                            PlanToTime = pr.PlanToTime,
                            CTGTestPropertyEntity = pr.CTGTestPropertyEntity,
                            CTGTestMethodEntity = pr.CTGTestMethodEntity,
                            IDTestRequirementEntity = pr.IDTestRequirementEntity,
                        }).ToList()
                    }).ToList(),
                });
        }

        public IQueryable<SYSRequirementInvoiceEntity> GetDetailTestRequirementById(long id)
        {
            return _context.SYSRequirementInvoice
                .Where(w => w.Id == id).Select(item => new SYSRequirementInvoiceEntity
                {
                    Id = item.Id,
                    RequirementTypeId = item.RequirementTypeId,
                    Serial = item.Serial,
                    SerialYear = item.SerialYear,
                    InvoiceNo = item.InvoiceNo,
                    Edition = item.Edition,
                    FieldId = item.FieldId,
                    CustomerId = item.CustomerId,
                    Representative = item.Representative,
                    RepresentativePhone = item.RepresentativePhone,
                    CreatedTime = item.CreatedTime,
                    CreatedBy = item.CreatedBy,
                    OtherInformation = item.OtherInformation,
                    IsSaveSpecimen = item.IsSaveSpecimen,
                    SaveSpecimenTime = item.SaveSpecimenTime,
                    IsUseSubcontractors = item.IsUseSubcontractors,
                    ResultDay = item.ResultDay,
                    ReturnInvoiceResultTypeId = item.ReturnInvoiceResultTypeId,
                    ResultInvoiceAmount = item.ResultInvoiceAmount,
                    ProcessStatusId = item.ProcessStatusId,
                    CTGReturnInvoiceResultTypeEntity = item.CTGReturnInvoiceResultTypeEntity,
                    IDTestRequirementEntities = item.IDTestRequirementEntities.Select(re => new IDTestRequirementEntity { 
                        Id = re.Id,
                        RequirementInvoiceId = re.RequirementInvoiceId,
                        ObjectId = re.ObjectId,
                        InvoiceResultSerial = re.InvoiceResultSerial,
                        InvoiceResultYear = re.InvoiceResultYear,
                        InvoiceResultNo = re.InvoiceResultNo,
                        InvoiceResultDate = re.InvoiceResultDate,
                        SpecimenName = re.SpecimenName,
                        SpecimenSymbol = re.SpecimenSymbol,
                        SpecimenOrder = re.SpecimenOrder,
                        SpecimenCode = re.SpecimenCode,
                        ImageLink = re.ImageLink,
                        SpecimenAmount = re.SpecimenAmount,
                        SpecimenStatus = re.SpecimenStatus,
                        SpecimenQuantum = re.SpecimenQuantum,
                        CTGTestObjectEntity = re.CTGTestObjectEntity,
                        IDTRTestPropertyEntities = re.IDTRTestPropertyEntities.Select(pr => new IDTRTestPropertyEntity
                        {
                            Id = pr.Id,
                            SpecimenId = pr.SpecimenId,
                            TestPropertyId = pr.TestPropertyId,
                            TestMethodId = pr.TestMethodId,
                            OrderNumber = pr.OrderNumber,
                            PlanFromTime = pr.PlanFromTime,
                            PlanToTime = pr.PlanToTime,
                            IDTRImplementerEntities = pr.IDTRImplementerEntities.Select(im => new IDTRImplementerEntity
                            {
                                Id = im.Id,
                                SpecimenPropertyId = im.SpecimenPropertyId,
                                UserId = im.UserId,
                                IsAccept = im.IsAccept,
                                TimeToStart = im.TimeToStart,
                                TimeToReport = im.TimeToReport,
                                SYSUserEntity = im.SYSUserEntity
                            }).ToList(),
                            CTGTestPropertyEntity = pr.CTGTestPropertyEntity,
                            CTGTestMethodEntities = pr.CTGTestPropertyEntity.CTGTestMethodEntities,
                            CTGTestMethodEntity = pr.CTGTestMethodEntity,
                            IDTestRequirementEntity = pr.IDTestRequirementEntity,
                        }).ToList()
                    }).ToList(),
                    CRESYSUserEntity = item.CRESYSUserEntity
                });
        }

        public IQueryable<IDTRImplementerEntity> GetDetailTestRequirementByImplementer(long userId)
        {
            return _context.IDTRImplementer
                .Include("SYSUserEntity")
                .Include("IDTRTestPropertyEntity")
                .Include("IDTRTestPropertyEntity.IDTestRequirementEntity")
                .Include("IDTRTestPropertyEntity.IDTestRequirementEntity.SYSRequirementInvoiceEntity")
                .Include("IDTRTestPropertyEntity.IDTestRequirementEntity.CTGTestObjectEntity")
                .Include("IDTRTestPropertyEntity.CTGTestPropertyEntity")
                .Include("IDTRTestPropertyEntity.CTGTestMethodEntity")
                .Include("IDTRTestPropertyEntity.IDTRImplementerEntities")
                .Include("IDTRTestPropertyEntity.IDTRImplementerEntities.SYSUserEntity")
                .Include("IDTRTestPropertyEntity.IDTRTestProcessWeightMethodEntities")
                .Include("IDTRTestPropertyEntity.IDTRTestProcessVolumeMethodEntities")
                .Include("IDTRTestPropertyEntity.IDTRTestProcessOtherMethodEntities")
                .Include("IDTRTestPropertyEntity.IDTRTestProcessAASUCVISAESMethodEntities")
                .Where(w => w.UserId == userId);
        }

        public IQueryable<SYSRequirementInvoiceEntity> GetByIdForSummary(long invoiceId)
        {
            return _context.SYSRequirementInvoice
                .Where(w => w.Id == invoiceId)
                .Select(item => new SYSRequirementInvoiceEntity
                {
                    Id = item.Id,
                    RequirementTypeId = item.RequirementTypeId,
                    Serial = item.Serial,
                    SerialYear = item.SerialYear,
                    InvoiceNo = item.InvoiceNo,
                    Edition = item.Edition,
                    FieldId = item.FieldId,
                    CustomerId = item.CustomerId,
                    Representative = item.Representative,
                    RepresentativePhone = item.RepresentativePhone,
                    OtherInformation = item.OtherInformation,
                    IsSaveSpecimen = item.IsSaveSpecimen,
                    SaveSpecimenTime = item.SaveSpecimenTime,
                    IsUseSubcontractors = item.IsUseSubcontractors,
                    CreatedTime = item.CreatedTime,
                    CreatedBy = item.CreatedBy,
                    ResultDay = item.ResultDay,
                    ReturnInvoiceResultTypeId = item.ReturnInvoiceResultTypeId,
                    ResultInvoiceAmount = item.ResultInvoiceAmount,
                    ProcessStatusId = item.ProcessStatusId,
                    CTGReturnInvoiceResultTypeEntity = item.CTGReturnInvoiceResultTypeEntity,
                    IDTestRequirementEntities = item.IDTestRequirementEntities.Select(re => new IDTestRequirementEntity
                    {
                        Id = re.Id,
                        RequirementInvoiceId = re.RequirementInvoiceId,
                        ObjectId = re.ObjectId,
                        InvoiceResultSerial = re.InvoiceResultSerial,
                        InvoiceResultYear = re.InvoiceResultYear,
                        InvoiceResultNo = re.InvoiceResultNo,
                        InvoiceResultDate = re.InvoiceResultDate,
                        SpecimenName = re.SpecimenName,
                        SpecimenSymbol = re.SpecimenSymbol,
                        SpecimenOrder = re.SpecimenOrder,
                        SpecimenCode = re.SpecimenCode,
                        ImageLink = re.ImageLink,
                        SpecimenAmount = re.SpecimenAmount,
                        SpecimenStatus = re.SpecimenStatus,
                        SpecimenQuantum = re.SpecimenQuantum,
                        CTGTestObjectEntity = re.CTGTestObjectEntity,
                        IDTRTestPropertyEntities = re.IDTRTestPropertyEntities.Select(pr => new IDTRTestPropertyEntity
                        {
                            Id = pr.Id,
                            SpecimenId = pr.SpecimenId,
                            TestPropertyId = pr.TestPropertyId,
                            TestMethodId = pr.TestMethodId,
                            OrderNumber = pr.OrderNumber,
                            PlanFromTime = pr.PlanFromTime,
                            PlanToTime = pr.PlanToTime,
                            IDTRImplementerEntities = pr.IDTRImplementerEntities.Select(im => new IDTRImplementerEntity
                            {
                                Id = im.Id,
                                SpecimenPropertyId = im.SpecimenPropertyId,
                                UserId = im.UserId,
                                IsAccept = im.IsAccept,
                                TimeToStart = im.TimeToStart,
                                TimeToReport = im.TimeToReport,
                                SYSUserEntity = im.SYSUserEntity
                            }).ToList(),
                            IDTRTestProcessWeightMethodEntities = pr.IDTRTestProcessWeightMethodEntities,
                            IDTRTestProcessVolumeMethodEntities = pr.IDTRTestProcessVolumeMethodEntities,
                            IDTRTestProcessOtherMethodEntities = pr.IDTRTestProcessOtherMethodEntities,
                            IDTRTestProcessAASUCVISAESMethodEntities = pr.IDTRTestProcessAASUCVISAESMethodEntities,
                            CTGTestPropertyEntity = pr.CTGTestPropertyEntity,
                            CTGTestMethodEntity = pr.CTGTestMethodEntity,
                            IDTestRequirementEntity = pr.IDTestRequirementEntity,
                        }).ToList()
                    }).ToList(),
                    CRESYSUserEntity = item.CRESYSUserEntity
                });
        }

        public IQueryable<SYSRequirementInvoiceEntity> GetFullByIdForUpdateTestRequirement(long invoiceId)
        {
            return _context.SYSRequirementInvoice
                .Include("IDTestRequirementEntities")
                .Include("IDTestRequirementEntities.IDTRTestPropertyEntities")
                .Include("IDTestRequirementEntities.IDTRTestPropertyEntities.IDTRImplementerEntities")
                .Include("IDTestRequirementEntities.IDTRTestPropertyEntities.IDTRTestProcessWeightMethodEntities")
                .Include("IDTestRequirementEntities.IDTRTestPropertyEntities.IDTRTestProcessVolumeMethodEntities")
                .Include("IDTestRequirementEntities.IDTRTestPropertyEntities.IDTRTestProcessOtherMethodEntities")
                .Include("IDTestRequirementEntities.IDTRTestPropertyEntities.IDTRTestProcessAASUCVISAESMethodEntities")
                .Where(w => w.Id == invoiceId);
        }
    }
}