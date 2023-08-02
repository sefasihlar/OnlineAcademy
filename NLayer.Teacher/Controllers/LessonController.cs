using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.LessonDtos;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IClassService _classService;
        private readonly IMapper _mapper;

        public LessonController(ILessonService lessonService, IClassService classService, IMapper mapper)
        {
            _lessonService=lessonService;
            _classService=classService;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _lessonService.GetWithClassList();

            if (values == null)
            {
                return NotFound();
            }

            return View(values);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var classes =await _classService.GetAllAsycn();
            ViewBag.classes = new SelectList(classes, "Id", "Name");

            if (classes == null)
            {
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Hata",
                    Message = "Bişeyler ters gitti.",
                    Css = "error"
                });
            }

            var values = await _lessonService.GetWithClassList();

            if (values == null)
            {
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Hata",
                    Message = "Ders Bulunamadı",
                    Css = "error"
                });
            }

            return View(values);
        }



        [HttpPost]
        public async Task<IActionResult> Create(LessonDto lessonDto)
        {

            var values = _mapper.Map<Lesson>(lessonDto);

            if (values != null)
            {
               await _lessonService.AddAsycn(values);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Ders başarıyla eklendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Lesson");
            }

            var classes = await _classService.GetAllAsycn();
            ViewBag.classes = new SelectList(classes, "Id", "Name");
            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Ders ekleme işlemi başarısız.Bilgilerinizi gözden geçiriniz",
                Css = "error"
            });
            return View(lessonDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int lessonId, int classId)
        {
            if (lessonId != null || classId != null)
            {
                try
                {
                    // İlişki yokken silme işlemi
                    _lessonService.DeleteFromLesson(lessonId, classId);
                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Başarılı",
                        Message = "Ders silme işlemi başarılı",
                        Css = "success"
                    });
                    return RedirectToAction("Index", "Lesson");
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

                        return RedirectToAction("Index", "Lesson");
                    }
                    else
                    {
                        TempData.Put("message", new ResultMessageDto()
                        {
                            Title = "Hata",
                            Message = "Ders silinemedi daha sonra tekrar deneyiniz",
                            Css = "error"
                        });
                    }
                }
            }
            return RedirectToAction("Index", "Lesson");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var values =await _lessonService.GetByIdAsycn(id);

            if (values == null)
            {
                return NotFound();
            }

            var lesson = _mapper.Map<LessonDto>(values);

            return View(lesson);

        }

        [HttpPost]
        public async Task<IActionResult> Update(LessonDto lessonDto)
        {
            var values =await _lessonService.GetByIdAsycn(lessonDto.Id);

            if (values == null)
            {
                return NotFound();
            }

            values.Name = lessonDto.Name;
            values.ClassId = lessonDto.ClassId;
            values.Condition = lessonDto.Condition;
            values.UpdatedDate = lessonDto.UpdatedDate;
            values.Condition = lessonDto.Condition;

            _lessonService.UpdateAsycn(values);

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Başarılı",
                Message = "Ders güncelleme işlemi başarılı",
                Css = "error"
            });
            return RedirectToAction("Index", "Lesson");
        }
    }
}
