using NLayer.Core.Abstract;
using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.DTOs.BranchDtos
{
    public class BranchDto : BaseDto
    {

        [Required(ErrorMessage = "En az 1 karakter içermelidir ")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "En az 1 karakter içermelidir ")]
        public string? Name { get; set; }
    }
}
