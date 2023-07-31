using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.RoleDtos
{
    public class RoleDto:BaseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
