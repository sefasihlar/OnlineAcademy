using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Concrate
{
    public class Class:BaseEntity
    {
        public string? Name { get; set; }
        public List<ClassBranch> ClassBranches { get; set; }
    }
}
