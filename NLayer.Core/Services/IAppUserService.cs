using NLayer.Core.Concrate;
using NLayer.Core.DTOs.AccountDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<List<AppUserDto>> ListTogether();
    }
}
