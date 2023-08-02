﻿using NLayer.Core.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ScorsDtos
{
    public class ScorListDto
    {
        public List<Scors> scors { get; set; }
        public int LessonId { get; set; }

        public string? ExamTitle { get; set; }
        public string? ExamDescription { get; set; }
        public string? ExamClass { get; set; }
        public int ExamDate { get; set; }
        public int ExamTimer { get; set; }

        public decimal TotalNullPercentile { get; set; }
        public decimal TotalFalsePercentile { get; set; }
        public decimal TotalTruePercentile { get; set; }
        public decimal TotalScorPercentile { get; set; }
    }
}