using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class OptionService : Service<Option>
    {
        public OptionService(IGenericRepository<Option> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
