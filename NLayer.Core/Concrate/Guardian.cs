using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class Guardian : BaseEntity
    {
        public string GurdianType { get; set; }
        public string GuardianName { get; set; }
        public string GuardianPhone { get; set; }
        public string GuardianMail { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
