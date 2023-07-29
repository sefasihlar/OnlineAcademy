using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Concrate
{
    public class Guardian:BaseEntity
    {
        public string GuardianName { get; set; }
        public string? GuardianName2 { get; set; }
        public string GuardianSurName { get; set; }
        public string? GuardianSurName2 { get; set; }
        public string GuardianPhone { get; set; }
        public string? GuardianPhone2 { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
