using NLayer.Core.Abstract;
using NLayer.Core.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.MessageDtos
{
    public class MessageDto:BaseDto
    {
        public string? Title { get; set; }
        public string? Text { get; set; }

        public int UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
