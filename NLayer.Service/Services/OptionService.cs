using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class OptionService : Service<Option>, IOptionService
    {
        public OptionService(IGenericRepository<Option> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
