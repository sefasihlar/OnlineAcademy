using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Concrate
{
    public class ClassBranch
    {
        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
