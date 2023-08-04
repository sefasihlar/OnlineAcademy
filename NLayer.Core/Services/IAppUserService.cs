using NLayer.Core.Concrate;
using NLayer.Core.DTOs.AccountDtos;
using NLayer.Core.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IAppUserService:IGenericService<AppUser>
    {
        Task<List<AppUserDto>> ListTogether();
    }
}
