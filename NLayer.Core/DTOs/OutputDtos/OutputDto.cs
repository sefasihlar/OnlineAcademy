using NLayer.Core.Abstract;
using NLayer.Core.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.OutputDtos
{
    public class OutputDto:BaseDto
    {
        public string Name { get; set; }

        public int UserId { get; set; }
        public AppUser? User { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
