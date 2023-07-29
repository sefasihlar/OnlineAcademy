using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ExamService : Service<Exam>
    {
        public ExamService(IGenericRepository<Exam> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
