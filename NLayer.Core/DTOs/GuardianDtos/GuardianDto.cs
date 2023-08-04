using NLayer.Core.Abstract;
using NLayer.Core.Concrate;

namespace NLayer.Core.DTOs.GuardianDtos
{
    public class GuardianDto : BaseDto
    {
        public string GurdianType { get; set; }
        public string GuardianName { get; set; }
        public string GuardianPhone { get; set; }
        public string GuardianMail { get; set; } 
        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
