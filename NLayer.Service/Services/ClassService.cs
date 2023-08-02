using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ClassBranchDtos;
using NLayer.Core.DTOs.ClassDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class ClassService : Service<Class>, IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;
        public ClassService(IGenericRepository<Class> repository, IUnitOfWork unitOfWork, IClassRepository classRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _classRepository=classRepository;
            _mapper=mapper;
        }

        public async Task<ClassDto> GetByIdWithBrances(int id)
        {
            var Class = await _classRepository.GetClassBranchList();
            var classDto = _mapper.Map<ClassDto>(Class);
            return classDto;
        }

        public async Task<List<ClassBranchDto>> GetClassBranchList()
        {
            var classbranch = await _classRepository.GetClassBranchList();
            var classbranchDto = _mapper.Map<List<ClassBranchDto>>(classbranch);
            return classbranchDto.ToList();
        }

        public void Update(ClassDto dto, int[] branchIds)
        {
            var entity = _mapper.Map<Class>(dto);

            _classRepository.Update(entity, branchIds);
        }
    }
}
