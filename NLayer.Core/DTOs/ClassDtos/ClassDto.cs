using NLayer.Core.Abstract;
using NLayer.Core.Concrate;
using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.DTOs.ClassDtos
{
    public class ClassDto : BaseDto
    {

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string? Name { get; set; }

        public List<Branch>? SelectedBranch { get; set; }
        public List<ClassBranch> ClassBranches { get; set; }

    }
}
