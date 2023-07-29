using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Concrate
{
    public class Message:BaseEntity
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
    }
}
