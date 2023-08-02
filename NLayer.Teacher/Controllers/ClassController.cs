using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ClassDtos;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;

        public ClassController(IClassService classService, IMapper mapper, IBranchService branchService)
        {
            _classService=classService;
            _mapper=mapper;
            _branchService=branchService;
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _branchService.GetAllAsycn();
            var classDto = _mapper.Map<List<ClassDto>>(classes);
            return View(classDto);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClassDto classDto)
        {
            if (classDto != null)
            {
                var values = await _classService.AddAsycn(_mapper.Map<Class>(classDto));

                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Basarili",
                    Message = "Ekleme islemi basariyla gerceklesti",
                    Css = "success"
                });
                return RedirectToAction("Index", "Class");
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Bişeyler ters gitti.Lütfen sınıf bilgilerini gözden geçirinizi",
                Css = "error"
            });


            return RedirectToAction("Index", "Class");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var values =await _classService.GetByIdAsycn(id);

            try
            {
                // İlişki yokken silme işlemi

                if (values != null)
                {
                    await _classService.RemoveAsycn(values);
                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Başarli",
                        Message = "Silme işlemi başarıyla gerçekleşti",
                        Css = "success"
                    });
                    return RedirectToAction("Index", "Class");
                }

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
                    return RedirectToAction("Index", "Class");
                }
                else
                {
                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Hata",
                        Message = "Şube silinemedi daha sonra tekrar deneyiniz",
                        Css = "error"
                    });
                }
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata!!!",
                Message = "Silme islemi gerceklestirilemedi",
                Css = "error"
            });

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var values =await _classService.GetByIdWithBrances(id);

            if (values == null)
            {
                return NotFound();
            }

            var branchList =await _branchService.GetAllAsycn();
                
            var branchFilter  = branchList.Where(a => a.Condition == true).ToList();

            ViewBag.Brances = branchFilter;


            // ! Burada Classbranches kısmında  tür dönüştürmesi hatasi alabiliriz.
            var classDto = new ClassDto()
            {
                Id = values.Id,
                Name = values.Name,
                Condition = values.Condition,
                SelectedBranch = values.ClassBranches.Select(x => x.Branch).ToList(),
            };

            return View(classDto);

        }

        [HttpPost]
        public async Task<IActionResult> Update(ClassDto classDto, int[] branchIds)
        {
            if (ModelState.IsValid)
            {

                var values = await _classService.GetByIdAsycn(classDto.Id);

                if (values == null)
                {
                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Hata",
                        Message = "Guncelleme islemi basarisiz sinifi bulamadik lütefen sonra tekrar deneyiniz",
                        Css = "error"
                    });
                }

                values.Name = classDto.Name;
                values.Condition = classDto.Condition;
                values.UpdatedDate = classDto.UpdatedDate;

                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Basarili",
                    Message = "Sinif basariyla guncellendi",
                    Css = "success"
                });

                var valuesDto = _mapper.Map<ClassDto>(values);

                _classService.Update(valuesDto, branchIds);

                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Sinif guncelleme islemi başarıyla gerçeklerşti",
                    Css = "success"
                });
                return RedirectToAction("Index", "Class");
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Sinif guncelleme islemi basarisiz lütfen bilgileri gozden geciriniz var eksik olmadıgına dikkat ediniz",
                Css = "error"
            });

            return RedirectToAction("Index", "Class");
        }
    }
}
