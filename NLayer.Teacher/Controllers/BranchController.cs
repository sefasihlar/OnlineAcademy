using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.BranchDtos;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class BranchController : Controller
    {

        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;

        public BranchController(IBranchService branchService, IMapper mapper)
        {
            _branchService=branchService;
            _mapper=mapper;
        }

        public async  Task<IActionResult> Index()
        {
            var branches = await _branchService.GetAllAsycn();
            var branchsDto = _mapper.Map<List<BranchDto>>(branches);
            return View(branchsDto);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BranchDto branchDto)
        {
            if (ModelState.IsValid)
            {
                if (branchDto == null)
                {
                    return NotFound(branchDto);
                }

                var branch = await _branchService.AddAsycn(_mapper.Map<Branch>(branchDto));

                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Basarili",
                    Message = "Sube basariyla eklendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Branch");
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Opps ! Biseyler ters gitti",
                Message = "Lutfen Şube bilgilerini tekrar gozden gecirinizi.",
                Css = "error"
            });
            return RedirectToAction("Index", "Branch", branchDto);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var values = await _branchService.GetByIdAsycn(id);
            if (values != null)
            {
                try
                {
                    // İlişki yokken silme işlemi
                    await _branchService.RemoveAsycn(values);
                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Basarili",
                        Message = "Sube basariyla silindi",
                        Css = "success"
                    });
                    return RedirectToAction("Index", "Branch");
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
                            Message = "Şube silinemedi daha sonra tekrar deneyiniz",
                            Css = "error"
                        });
                    }
                }

                return RedirectToAction("Index", "Branch");
            }
            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Opps!Biseyler ters gitti daha sonra tekrar deneyiniz.",
                Css = "error"
            });
            return View("Index", "Branch");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var values = await _branchService.GetByIdAsycn(id);

            if (values == null)
            {

                return NotFound();
            }

            var valuesDto = _mapper.Map<BranchDto>(values);

           return View(valuesDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BranchDto branchDto)
        {

            var values = await _branchService.GetByIdAsycn(branchDto.Id);
            if (values == null)
            {
                return NotFound();
            }

            else
            {
                values.Name = branchDto.Name;
                values.Condition = branchDto.Condition;
                values.UpdatedDate = branchDto.UpdatedDate;
                await _branchService.UpdateAsycn(values);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Guncelleme basarili",
                    Message = "Sube guncelleme islemi bariyla gerceklesti",
                    Css = "success"
                });
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Sube guncellenemedi bilgileri tekrar gozden geciriniz.",
                Css = "error"
            });
            return RedirectToAction("Index", "Branch");
        }
    }
}
