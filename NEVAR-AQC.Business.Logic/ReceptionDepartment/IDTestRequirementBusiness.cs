using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NEVAR_AQC.Business.ReceptionDepartment;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Data.ReceptionDepartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.ReceptionDepartment
{
    public class IDTestRequirementBusiness : IIDTestRequirementBusiness
    {
        private IMapper _mapper;
        private IIDTestRequirementRepository _iDTestRequirementRepository;

        public IDTestRequirementBusiness(IMapper mapper,
            IIDTestRequirementRepository iDTestRequirementRepository)
        {
            _mapper = mapper;
            _iDTestRequirementRepository = iDTestRequirementRepository;
        }

        public Task<IEnumerable<IDTestRequirementModel>> GetByInvoiceAsync(long invoiceId, CancellationToken cancellationToken = default)
        {
            var data = _iDTestRequirementRepository.Find(w => w.RequirementInvoiceId == invoiceId, w => w.IDTRTestPropertyEntities);
            var result = _mapper.Map<IEnumerable<IDTestRequirementModel>>(data);
            return Task.FromResult(result);
        }

        public Task<IDTestRequirementModel> GetByIdForReportAsync(long specimentId)
        {
            //Todo: Update invoice result no here


            var data = _iDTestRequirementRepository.TestRequirementReport(specimentId).FirstOrDefault();
            var result = _mapper.Map<IDTestRequirementModel>(data);
            return Task.FromResult(result);
        }

        public async Task<long> GetLastResultSerialAsync(int year)
        {
            var query = _iDTestRequirementRepository.Find(w => w.InvoiceResultYear == year);
            var result = await query.OrderByDescending(w => w.InvoiceResultSerial).FirstOrDefaultAsync();
            return result?.InvoiceResultSerial ?? 0;
        }

        public Task UpdateResultNo(long specimentId, long invoiceResultSerial, int invoiceResultYear, string invoiceResultNo, DateTime invoiceResultDate)
        {
            var item = _iDTestRequirementRepository.FindSingle(x => x.Id == specimentId);
            item.InvoiceResultSerial = invoiceResultSerial;
            item.InvoiceResultYear = invoiceResultYear;
            item.InvoiceResultNo = invoiceResultNo;
            item.InvoiceResultDate = invoiceResultDate;

            _iDTestRequirementRepository.Update(item);

            _iDTestRequirementRepository.SaveChange();

            return Task.CompletedTask;
        }
    }
}