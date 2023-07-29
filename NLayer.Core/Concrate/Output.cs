using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NLayer.Core.Concrate
{
    public class Output:BaseEntity
    {
        public string Name { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

    }
}
