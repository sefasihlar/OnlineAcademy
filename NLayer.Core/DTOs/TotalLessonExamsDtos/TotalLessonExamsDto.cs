using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.TotalLessonExamsDtos
{
    public class TotalLessonExamsDto
    {
        public int TotalMatematik { get; set; }
        public int TotalTukce { get; set; }
        public int TotalFizik { get; set; }
        public int TotalTarih { get; set; }
        public int TotalBiyoloji { get; set; }
        public int TotalKimya { get; set; }
        public int TotalCografya { get; set; }
        public int TotalEdebiyat { get; set; }
        public int TotalFelsefe { get; set; }
    }
}
