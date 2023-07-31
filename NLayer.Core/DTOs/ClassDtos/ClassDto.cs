using NLayer.Core.Abstract;
using NLayer.Core.Concrate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace NLayer.Core.DTOs.ClassDtos
{
    public class ClassDto:BaseDto
    {

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string? Name { get; set; }

        public List<Branch>? SelectedBranch { get; set; }

    }
}
