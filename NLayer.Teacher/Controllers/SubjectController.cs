using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.DTOs.SubjectDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly ILessonService _lessonService;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectService subjectService, ILessonService lessonService, IMapper mapper)
        {
            _subjectService=subjectService;
            _lessonService=lessonService;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _subjectService.GetWithLessonList();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubjectDto subjectDto)
        {
            if (subjectDto.Name == null || subjectDto.LessonId == null)
            {
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Hata",
                    Message = "Ders bulunamadı.Lütfen bilgileri gözden geçiriniz",
                    Css = "error"
                });
            }

            var values = _mapper.Map<Subject>(subjectDto);

            if (values != null)
            {
                await _subjectService.AddAsycn(values);

                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Konu ekleme işlemi başarılı",
                    Css = "success"
                });

                return RedirectToAction("Index", "Subject");
            }

            var lesson = await _lessonService.GetAllAsycn();

            ViewBag.lessons = new SelectList(lesson, "Id", "Name");

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Konu ekleme işlemi başarısız.Lütefen bilgileri gözden geçiriniz",
                Css = "error"
            });

            return View(subjectDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int subjectId, int lessonId)
        {
            try
            {
                // İlişki yokken silme işlemi
                _subjectService.DeleteFromSubject(subjectId, lessonId);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Silme işlemi başarılı",
                    Css = "success"
                });
                return RedirectToAction("Index", "Subject");
            }
            catch (Exception ex)
            {
                // İlişkili kayıt olduğunda hata mesajı
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Hata",
                        Message = "Silmeye çalıştığınız kayıt, başka bir tablodaki kayıtlarla ilişkili olduğu için silinemiyor. Lütfen önce ilişkili kayıtları silin veya düzenleyin ve daha sonra tekrar deneyin.",
                        Css = "error"
                    });
                }
                else
                {
                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Hata",
                        Message = "Silmeye çalışılan konu bir ilişki içerisinde.Lütefen önce ilişili olduğu veriyi siliniz ve daha sonra tekrar deneyiniz",
                        Css = "error"
                    });
                }
            }

            return RedirectToAction("Index", "Branch");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(SubjectDto subjectDto)
        {
            var values = await _subjectService.GetByIdAsycn(subjectDto.Id);

            if (values == null)
            {
                return NotFound();
            }

            var valuesDto = _mapper.Map<SubjectDto>(values);

            return View(valuesDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SubjectDto subjectDto)
        {
            var values = await _subjectService.GetByIdAsycn(subjectDto.Id);

            if (values == null)
            {
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Hata",
                    Message = "Konu güncellme işlemi başarısız.Lütefen daha sonra tekrar deneyiniz",
                    Css = "error"
                });
            }

            values.Name = subjectDto.Name;
            values.LessonId = subjectDto.LessonId;
            values.Condition = subjectDto.Condition;
            values.UpdatedDate = subjectDto.UpdatedDate;

            await _subjectService.UpdateAsycn(values);

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Konu güncelleme işlemi başarılı",
                Css = "success"
            });
            return RedirectToAction("Index", "Subject");
        }
    }
}
