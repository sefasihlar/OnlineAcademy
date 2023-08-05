using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.AccountDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class AppUserService : Service<AppUser>, IAppUserService
    {
        private readonly IAppUserRepository _appUserrepository;
        private readonly IMapper _mapper;
        public AppUserService(IGenericRepository<AppUser> repository, IUnitOfWork unitOfWork, IMapper mapper, IAppUserRepository appUserrepository) : base(repository, unitOfWork)
        {
            _mapper=mapper;
            _appUserrepository=appUserrepository;
        }

        public async Task<List<AppUserDto>> ListTogether()
        {
            var values = await _appUserrepository.ListTogether();
            var valuesDto = _mapper.Map<List<AppUserDto>>(values);
            return valuesDto;
        }
    }
}
