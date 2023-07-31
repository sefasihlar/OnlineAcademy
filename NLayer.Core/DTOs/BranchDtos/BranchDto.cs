using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace NLayer.Core.DTOs.BranchDtos
{
    public class BranchDto:BaseDto
    {

        [Required(ErrorMessage = "En az 1 karakter içermelidir ")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "En az 1 karakter içermelidir ")]
        public string? Name { get; set; }
    }
}
