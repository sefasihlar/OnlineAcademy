using NLayer.Core.Abstract;
using System.ComponentModel.DataAnnotations;

namespace NLayer.Core.DTOs.RoleDtos
{
    public class RoleDto : BaseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
