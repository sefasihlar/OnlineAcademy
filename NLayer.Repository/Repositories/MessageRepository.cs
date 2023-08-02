using NLayer.Core.Concrate;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class MessageRepository : GenericRepositoy<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
