using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class MessageService : Service<Message>
    {
        public MessageService(IGenericRepository<Message> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
