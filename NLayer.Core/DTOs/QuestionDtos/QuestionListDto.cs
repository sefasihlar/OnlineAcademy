﻿using NLayer.Core.Concrate;
using NLayer.Core.DTOs.LessonDtos;

namespace NLayer.Core.DTOs.QuestionDtos
{
    public class QuestionListDto
    {
        public List<Question> Questions { get; set; }
        public List<Class> Classes { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<LessonDto> Lessons { get; set; }
        public List<Output> Outputs { get; set; }
        public int SureDegeri { get; set; }

        public List<int> SelectedQuestions { get; set; }
    }
}
