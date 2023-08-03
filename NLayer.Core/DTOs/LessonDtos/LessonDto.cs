using NLayer.Core.Abstract;
using NLayer.Core.Concrate;

namespace NLayer.Core.DTOs.LessonDtos
{
    public class LessonDto : BaseDto
    {
        public string? Name { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        public int UserId { get; set; }
        public virtual AppUser? User { get; set; }
    }
}
