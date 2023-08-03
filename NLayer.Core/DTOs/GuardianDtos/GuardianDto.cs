using NLayer.Core.Abstract;
using NLayer.Core.Concrate;

namespace NLayer.Core.DTOs.GuardianDtos
{
    public class GuardianDto : BaseDto
    {
        public string GuardianName { get; set; }
        public string? GuardianName2 { get; set; }
        public string GuardianSurName { get; set; }
        public string? GuardianSurName2 { get; set; }
        public string GuardianPhone { get; set; }
        public string? GuardianPhone2 { get; set; }

        public string? Email { get; set; }
        public string? Email2 { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
