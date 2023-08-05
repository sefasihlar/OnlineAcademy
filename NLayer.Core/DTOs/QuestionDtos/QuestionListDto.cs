using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ClassDtos;
using NLayer.Core.DTOs.LessonDtos;
using NLayer.Core.DTOs.OutputDtos;
using NLayer.Core.DTOs.SubjectDtos;

namespace NLayer.Core.DTOs.QuestionDtos
{
    public class QuestionListDto
    {
        public List<QuestionDto> Questions { get; set; }
        public List<ClassDto> Classes { get; set; }
        public List<SubjectDto> Subjects { get; set; }
        public List<LessonDto> Lessons { get; set; }
        public List<OutputDto> Outputs { get; set; }
        public int SureDegeri { get; set; }

        public List<int> SelectedQuestions { get; set; }
    }
}
