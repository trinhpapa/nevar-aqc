using AutoMapper;
using NEVAR_AQC.Business.SystemLog;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.System;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Data.SystemLog;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.SystemLog
{
    public class LOGLoginBusiness : ILOGLoginBusiness
    {
        private IMapper _mapper;
        private ILOGLoginRepository _lOGLoginRepository;

        public LOGLoginBusiness(IMapper mapper,
            ILOGLoginRepository lOGLoginRepository)
        {
            _mapper = mapper;
            _lOGLoginRepository = lOGLoginRepository;
        }

        public Task CreateAsync(LOGLoginModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<LOGLoginEntity>(model);

            _lOGLoginRepository.Create(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _lOGLoginRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task<PagedResult<LOGLoginModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString)
        {
            var query = _lOGLoginRepository.Find(null);

            if (searchString != null)
            {
                query = query.Where(w => w.Username == searchString);
            }

            var totalRow = query.Count();

            query = query.OrderByDescending(w => w.LoginTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var data = _mapper.Map<IList<LOGLoginModel>>(query);

            return Task.FromResult(new PagedResult<LOGLoginModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            });
        }

        public Task<IEnumerable<LOGLoginModel>> GetTopAsync(int record = 10)
        {
            var query = _lOGLoginRepository.Find(null).OrderByDescending(w => w.LoginTime).Take(record);

            return Task.FromResult(_mapper.Map<IEnumerable<LOGLoginModel>>(query));
        }
    }
}