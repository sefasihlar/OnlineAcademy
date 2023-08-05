using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.AccountDtos;
using NLayer.Core.DTOs.GuardianDtos;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IClassService _classService;
        private readonly IBranchService _branchService;
        private readonly ICartService _cartService;
        private readonly IGuardianService _guardianService;
        private readonly IMapper _mapper;

        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(IAppUserService appUserService, IClassService classService, IBranchService branchService, ICartService cartService, IGuardianService guardianService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _appUserService = appUserService;
            _classService = classService;
            _branchService = branchService;
            _cartService = cartService;
            _guardianService = guardianService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        //private readonly IEmailSender _emailSender;


        public async Task<IActionResult> Index(int ClassId, int BranchId, string Tc)
        {
            var values = await _appUserService.ListTogether();

            if (ClassId != 0 && BranchId != 0)
            {


                return View(new AppUserListDto()
                {
                    //async metod oldugu icin hata alabiriz ! 
                    Users = values
                    .Where(a => a.ClassId == ClassId)
                    .Where(a => a.BranchId == BranchId)
                    .Where(x => x.Authority == false)
                    .ToList(),

                }); ;
            }


            else if (ClassId != 0 && BranchId == 0)
            {
                return View(new AppUserListDto()
                {
                    //async metod oldugu icin hata alabiriz ! 
                    Users = values
                       .Where(a => a.ClassId == ClassId)
                       .Where(x => x.Authority == false)
                       .ToList(),

                });
            }

            else if (ClassId == 0 && BranchId != 0)
            {
                return View(new AppUserListDto()
                {
                    //async metod oldugu icin hata alabiriz ! 
                    Users = values
                       .Where(a => a.BranchId == BranchId)
                       .Where(x => x.Authority == false)
                       .ToList(),

                });
            }

            else if (Tc != null)
            {
                return View(new AppUserListDto()
                {
                    //async metod oldugu icin hata alabiriz ! 
                    Users = values
                       .Where(a => a.Tc == Tc)
                       .ToList(),

                });
            }

            var AuthFilter = await _appUserService.ListTogether();
            var AuthFilterUser = AuthFilter.Where(x => x.Authority == false).ToList();

            return View(new AppUserListDto()
            {
                //async metod oldugu icin hata alabiriz ! 
                Users = AuthFilterUser

            });

        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var classes = await _classService.GetAllAsycn();
            ViewBag.classes = new SelectList(classes, "Id", "Name");

            var brances = await _branchService.GetAllAsycn();
            ViewBag.brances = new SelectList(brances, "Id", "Name");

            if (classes == null || brances == null)
            {
                return RedirectToAction("404", "Error");
            }


            var values = new AppUserListDto()
            {
                Users =await _appUserService.ListTogether()
            };

            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto, GuardianDto guardian)
        {

            AppUser user = new AppUser()
            {
                Id = dto.Id,
                Tc = dto.TcNumber,
                Name = dto.Name,
                SurName = dto.SurName,
                ClassId = dto.ClassId,
                BranchId = dto.BranchId,
                Gender = (bool)dto.Gender,
                PasswordHash = dto.Password,
                Condition = true,
                UserName = Convert.ToString(dto.TcNumber),
                NormalizedEmail = "",
                PhoneNumber = dto.Phone,

            };


            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                // kullanıcı başarıyla kaydedildi         
                var _guardian = _mapper.Map<Guardian>(guardian);

                if (_guardian != null)
                {
                    await _guardianService.AddAsycn(_guardian);
                }

                await _userManager.AddToRoleAsync(user, "Öğrenci");
            }
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    Token = code,
                });

                //Burası email gönderme kısmı(send Email)

                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Kullanıcı başarıyla oluşturuldu",
                    Css = "success"
                });

                return RedirectToAction("Index", "Account");
            }

            //ModelState.AddModelError("", "Kullanıcı oluşturlamadı! Lütfen bilgileri tekrar gözden geçiriniz");

            var classes = await _classService.GetAllAsycn();
            ViewBag.classes = new SelectList(classes, "Id", "Name");

            var brances = await _branchService.GetAllAsycn();
            ViewBag.brances = new SelectList(brances, "Id", "Name");

            if (classes == null || brances == null)
            {
                return RedirectToAction("404", "Error");
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Kullanıcı oluşturulamadı.Lütefen bilgileri gözden geçiriniz",
                Css = "error"
            });

            return RedirectToAction("Index", "Account", dto);
        }

        public async Task<IActionResult> UpdateUser(AppUserDto dto, GuardianDto guardianDto)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(dto.Id));
            if (user != null)
            {
                user.Tc = dto.Tc;
                user.Name = dto.Name;
                user.SurName = dto.SurName;
                user.ClassId = dto.ClassId;
                user.BranchId = dto.BranchId;
                user.Gender = (bool)dto.Gender;
                user.PhoneNumber = dto.PhoneNumber;
                user.UserName = dto.UserName;
                user.Email = dto.Email;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var guardianList = await _guardianService.GetWithStudentList();
                var guardian = guardianList.FirstOrDefault(x => x.UserId == dto.Id);
                var guardianId = await _guardianService.GetByIdAsycn(guardian.Id);
                

                await _guardianService.UpdateAsycn(guardianId);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Kullanıcı güncelleme işlemi başarılı",
                    Css = "success"
                });
                return RedirectToAction("Index", "Account");
            }


            return View(dto);

        }

        public async Task<IActionResult> UserDetail()
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            var getId = await _appUserService.GetByIdAsycn(Convert.ToInt32(userId));

            var valuesList = await _appUserService.ListTogether();

            var values = valuesList.FirstOrDefault(x => x.Id == getId.Id);

            var user = new AppUserDto()
            {
                Tc = values.Tc,
                Name = values.Name,
                SurName = values.SurName,
                UserName = values.UserName,
                Email = values.Email,
                Class = values.Class,
                Branch = values.Branch,
                Gender = values.Gender,
                PasswordHash = values.PasswordHash,
                PhoneNumber = values.PhoneNumber,

            };


            return View(user);
        }


        public async Task<IActionResult> StudentDetail(int id)
        {

            var valuesList = await _appUserService.ListTogether();
            var values = valuesList.FirstOrDefault(x => x.Id == id);

            var userguardianList = await _guardianService.GetWithStudentList();
            var userguardian = userguardianList.FirstOrDefault(x => x.UserId == id);

            ViewBag.GuardianId = userguardian.Id;

            var user = new AppUserDto()
            {
                Id = id,
                Tc = values.Tc,
                Name = values.Name,
                SurName = values.SurName,
                UserName = values.UserName,
                Email = values.Email,
                Class = values.Class,
                Branch = values.Branch,
                Gender = values.Gender,
                PasswordHash = values.PasswordHash,
                PhoneNumber = values.PhoneNumber,

            };


            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            //email e göre değil tc ye göre giriş yapılacak
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null)
            {
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Hata",
                    Message = "Giris basarisiz Lütfen bilgilerinizi gozden geciriniz",
                    Css = "error"
                });
                return View(dto);
            }

            var userValues = new AppUserDto()
            {
                Name = user.Name,
                SurName = user.SurName,
            };


            ViewBag.userValues = userValues;


            if (!await _userManager.IsEmailConfirmedAsync(user))
            {

                // Email eklenmediği zaman Email oluşturma sayfasına yönlendirilecek
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Uyarı!",
                    Message = "Sisteme giriş yapabilmek için  Eposta oluşturun",
                    Css = "warning"
                });
                return RedirectToAction("CreateEmail", "Account", dto);
            }

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true, false);


            //Kullanıcınin hesabi başarıyla onlaylandı ise giriş yapabilecek 
            if (result.Succeeded)
            {
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Basarili",
                    Message = "Giris Basarili",
                    Css = "success"
                });

                if (await _userManager.IsInRoleAsync(user, "Öğrenci"))
                {
                    return RedirectToAction("StudentIndex", "Home");
                }

                return RedirectToAction("Index", "Home");
            }
            //diğer durumda hesap onaylanmadan giriş yapamacak
            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Giriş başarısız !!!",
                Message = "Tc kimlik numarası yada Sifre yanlis",
                Css = "error"
            });


            return View(dto);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Oturum",
                Message = "Hesabınınzdan güvenli bir sekilde cikis yapıldı",
                Css = "warning"
            });


            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {

            if (userId == null || token == null)
            {
                //tempdata "Hesap onayi icin bilgileriniz yanlis"
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Hesap Onayı",
                    Message = "Hesap Onaylanamadı",
                    Css = "error"
                });
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Hesap Onayı",
                    Message = "Kullanıcı Bulunamadı",
                    Css = "error"
                });
                return View();
            }

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    //onaylama islemi basarili ise kullaniciya kart tanimlansin
                    _cartService.InitializeCart(Convert.ToString(user.Id));
                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Hesap Onayı",
                        Message = "Hesabınız onaylandı :))",
                        Css = "success"
                    });
                    return RedirectToAction("Login", "Account");
                }
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hesap Onayı",
                Message = "Hesabınızı Onaylanamadı",
                Css = "error"
            });
            return View();

            //tempdata ile hata mesaji goster
        }

        [HttpGet]

        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Sifreyi Unuttum",
                    Message = "Bilgileriniz hatalı !!!",
                    Css = "error"
                });
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Sifreyi Unuttum",
                    Message = "Kullanıcı Bulunamadı!",
                    Css = "error"
                });
                return View();

            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action("ResetPassword", "Account", new
            {

                Token = code

            });
            //Mail burada gönderiliyor
            //await _emailSender.SendEmailAsync(Email, "Parolayı yenile", $"<!DOCTYPE html>\r\n<html>\r\n\r\n<head>\r\n<title></title>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n<style type=\"text/css\">\r\n@media screen {{\r\n@font-face {{\r\nfont-family: 'Lato';\r\nfont-style: normal;\r\nfont-weight: 400;\r\nsrc: local('Lato Regular'), local('Lato-Regular'), url(https://fonts.gstatic.com/s/lato/v11/qIIYRU-oROkIk8vfvxw6QvesZW2xOQ-xsNqO47m55DA.woff) format('woff');\r\n}}\r\n\r\n@font-face {{\r\nfont-family: 'Lato';\r\n    font-style: normal;\r\n                font-weight: 700;\r\n                src: local('Lato Bold'), local('Lato-Bold'), url(https://fonts.gstatic.com/s/lato/v11/qdgUG4U09HnJwhYI-uK18wLUuEpTyoUstqEm5AMlJo4.woff) format('woff');\r\n}}\r\n\r\n@font-face {{\r\nfont-family: 'Lato';\r\nfont-style: italic;\r\nfont-weight: 400;\r\nsrc: local('Lato Italic'), local('Lato-Italic'), url(https://fonts.gstatic.com/s/lato/v11/RYyZNoeFgb0l7W3Vu1aSWOvvDin1pK8aKteLpeZ5c0A.woff) format('woff');\r\n}}\r\n\r\n@font-face {{\r\nfont-family: 'Lato';\r\nfont-style: italic;\r\nfont-weight: 700;\r\nsrc: local('Lato Bold Italic'), local('Lato-BoldItalic'), url(https://fonts.gstatic.com/s/lato/v11/HkF_qI1x_noxlxhrhMQYELO3LdcAZYWl9Si6vvxL-qU.woff) format('woff');\r\n}}\r\n}}\r\n\r\n/* CLIENT-SPECIFIC STYLES */\r\nbody,\r\ntable,\r\ntd,\r\na {{\r\n-webkit-text-size-adjust: 100%;\r\n-ms-text-size-adjust: 100%;\r\n}}\r\n\r\ntable,\r\ntd {{\r\nmso-table-lspace: 0pt;\r\nmso-table-rspace: 0pt;\r\n}}\r\n\r\nimg {{\r\n-ms-interpolation-mode: bicubic;\r\n}}\r\n\r\n/* RESET STYLES */\r\nimg {{\r\nborder: 0;\r\nheight: auto;\r\nline-height: 100%;\r\noutline: none;\r\ntext-decoration: none;\r\n}}\r\n\r\ntable {{\r\nborder-collapse: collapse !important;\r\n}}\r\n\r\nbody {{\r\nheight: 100% !important;\r\nmargin: 0 !important;\r\npadding: 0 !important;\r\nwidth: 100% !important;\r\n}}\r\n\r\n/* iOS BLUE LINKS */\r\na[x-apple-data-detectors] {{\r\ncolor: inherit !important;\r\ntext-decoration: none !important;\r\nfont-size: inherit !important;\r\nfont-family: inherit !important;\r\nfont-weight: inherit !important;\r\nline-height: inherit !important;\r\n}}\r\n\r\n/* MOBILE STYLES */\r\n@media screen and (max-width:600px) {{\r\nh1 {{\r\nfont-size: 32px !important;\r\nline-height: 32px !important;\r\n}}\r\n}}\r\n\r\n/* ANDROID CENTER FIX */\r\ndiv[style*=\"margin: 16px 0;\"] {{\r\nmargin: 0 !important;\r\n}}\r\n</style>\r\n</head>\r\n\r\n<body style=\"background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\">\r\n<!-- HIDDEN PREHEADER TEXT -->\r\n<div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: 'Lato', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\">Burada olmanızdan heyecan duyuyoruz!\r\n</div>\r\n<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n<!-- LOGO -->\r\n<tr>\r\n<td bgcolor=\"#007fff\" align=\"center\">\r\n<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n<tr>\r\n<td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>\r\n<tr>\r\n<td bgcolor=\"#007fff\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\">\r\n<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n<tr>\r\n<td bgcolor=\"#ffffff\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;\">\r\n<h1 style=\"font-size: 48px; font-weight: 400; margin: 2;\">Şifreyi Sıfırla</h1> <img src=\" https://img.icons8.com/clouds/100/000000/handshake.png\" width=\"125\" height=\"120\" style=\"display: block; border: 0px;\" />\r\n</td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>\r\n<tr>\r\n<td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\">\r\n<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n<tr>\r\n<td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 40px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n<p style=\"margin: 0;\">Şifreyi yenilemek için aşşağıdaki butona basınız.</p>\r\n</td>\r\n</tr>\r\n<tr>\r\n<td bgcolor=\"#ffffff\" align=\"left\">\r\n<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n<tr>\r\n<td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 20px 30px 60px 30px;\">\r\n<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n<tr>\r\n<td align=\"center\" style=\"border-radius: 3px;\" bgcolor=\"#007fff\"><a href='https://localhost:44382{callbackUrl}' target=\"_blank\" style=\"font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; border: 1px solid #007fff; display: inline-block;\">Şifreyi Yenile</a></td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr> <!-- COPY -->\r\n<tr>\r\n<td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 0px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n\r\n</td>\r\n</tr> <!-- COPY -->\r\n<tr>\r\n<td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n<p style=\"margin: 0;\"><a href=\"#\" target=\"_blank\" style=\"color: #007fff;\">https://www.metaakdeniz.com</a></p>\r\n</td>\r\n</tr>\r\n<tr>\r\n<td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n                           \r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n                            <p style=\"margin: 0;\">Meta Akdeniz,<br>META Team</p>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 30px 10px 0px 10px;\">\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n                    <tr>\r\n                        <td bgcolor=\"#007fff\" align=\"center\" style=\"padding: 30px 30px 30px 30px; border-radius: 4px 4px 4px 4px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n                            <h2 style=\"font-size: 20px; font-weight: 400; color:white; margin: 0;\">ACADEMY</h2>\r\n                            <p style=\"margin: 0;\"><a href=\"#\" target=\"_blank\" style=\"color: white;\">Yardımmı Lazım ? </a></p>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\">\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n                    <tr>\r\n                        <td bgcolor=\"#f4f4f4\" align=\"left\" style=\"padding: 0px 30px 30px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;\"> <br>\r\n                            <p style=\"margin: 0;\">Tüm Haklar Saklıdır . <a href=\"#\" target=\"_blank\" style=\"color: #111111; font-weight: 700;\">www.metaakdeniz.com</a>.</p>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</body>\r\n\r\n</html>");
            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Şifre Yenileme",
                Message = "Hesabiniza gelen link üzerinde sifreyi yenileyebilirsiniz :))",
                Css = "warning"
            });

            return RedirectToAction("ResetPassword", "Account");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            if (token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPasswordModel { Token = token };

            return View(model);

        }

        [HttpPost]

        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateEmail(LoginDto dto)
        {
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmail(AppUserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                user.Email = model.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                    {
                        userId = user.Id,
                        Token = code,
                    });

                    //Burası email gönderme kısmı(send Email)

                    //await _emailSender.SendEmailAsync(model.Email, "Hesabı Onayla", $"<!DOCTYPE html>\r\n<html>\r\n\r\n<head>\r\n    <title></title>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n    <style type=\"text/css\">\r\n        @media screen {{\r\n            @font-face {{\r\n                font-family: 'Lato';\r\n                font-style: normal;\r\n                font-weight: 400;\r\n                src: local('Lato Regular'), local('Lato-Regular'), url(https://fonts.gstatic.com/s/lato/v11/qIIYRU-oROkIk8vfvxw6QvesZW2xOQ-xsNqO47m55DA.woff) format('woff');\r\n            }}\r\n\r\n            @font-face {{\r\n                font-family: 'Lato';\r\n                font-style: normal;\r\n                font-weight: 700;\r\n                src: local('Lato Bold'), local('Lato-Bold'), url(https://fonts.gstatic.com/s/lato/v11/qdgUG4U09HnJwhYI-uK18wLUuEpTyoUstqEm5AMlJo4.woff) format('woff');\r\n            }}\r\n\r\n            @font-face {{\r\n                font-family: 'Lato';\r\n                font-style: italic;\r\n                font-weight: 400;\r\n                src: local('Lato Italic'), local('Lato-Italic'), url(https://fonts.gstatic.com/s/lato/v11/RYyZNoeFgb0l7W3Vu1aSWOvvDin1pK8aKteLpeZ5c0A.woff) format('woff');\r\n            }}\r\n\r\n            @font-face {{\r\n                font-family: 'Lato';\r\n                font-style: italic;\r\n                font-weight: 700;\r\n                src: local('Lato Bold Italic'), local('Lato-BoldItalic'), url(https://fonts.gstatic.com/s/lato/v11/HkF_qI1x_noxlxhrhMQYELO3LdcAZYWl9Si6vvxL-qU.woff) format('woff');\r\n            }}\r\n        }}\r\n\r\n        /* CLIENT-SPECIFIC STYLES */\r\n        body,\r\n        table,\r\n        td,\r\n        a {{\r\n            -webkit-text-size-adjust: 100%;\r\n            -ms-text-size-adjust: 100%;\r\n        }}\r\n\r\n        table,\r\n        td {{\r\n            mso-table-lspace: 0pt;\r\n            mso-table-rspace: 0pt;\r\n        }}\r\n\r\n        img {{\r\n            -ms-interpolation-mode: bicubic;\r\n        }}\r\n\r\n        /* RESET STYLES */\r\n        img {{\r\n            border: 0;\r\n            height: auto;\r\n            line-height: 100%;\r\n            outline: none;\r\n            text-decoration: none;\r\n        }}\r\n\r\n        table {{\r\n            border-collapse: collapse !important;\r\n        }}\r\n\r\n        body {{\r\n            height: 100% !important;\r\n            margin: 0 !important;\r\n            padding: 0 !important;\r\n            width: 100% !important;\r\n        }}\r\n\r\n        /* iOS BLUE LINKS */\r\n        a[x-apple-data-detectors] {{\r\n            color: inherit !important;\r\n            text-decoration: none !important;\r\n            font-size: inherit !important;\r\n            font-family: inherit !important;\r\n            font-weight: inherit !important;\r\n            line-height: inherit !important;\r\n        }}\r\n\r\n        /* MOBILE STYLES */\r\n        @media screen and (max-width:600px) {{\r\n            h1 {{\r\n                font-size: 32px !important;\r\n                line-height: 32px !important;\r\n            }}\r\n        }}\r\n\r\n        /* ANDROID CENTER FIX */\r\n        div[style*=\"margin: 16px 0;\"] {{\r\n            margin: 0 !important;\r\n        }}\r\n    </style>\r\n</head>\r\n\r\n<body style=\"background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\">\r\n    <!-- HIDDEN PREHEADER TEXT -->\r\n    <div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: 'Lato', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\"> Burada olmanızdan heyecan duyuyoruz! Yeni hesabınıza dalmaya hazır olun.\r\n    </div>\r\n    <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n        <!-- LOGO -->\r\n        <tr>\r\n            <td bgcolor=\"#007fff\" align=\"center\">\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n                    <tr>\r\n                        <td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#007fff\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\">\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n                    <tr>\r\n                        <td bgcolor=\"#ffffff\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;\">\r\n                            <h1 style=\"font-size: 48px; font-weight: 400; margin: 2;\">Hoşgeldin!</h1> <img src=\" https://img.icons8.com/clouds/100/000000/handshake.png\" width=\"125\" height=\"120\" style=\"display: block; border: 0px;\" />\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\">\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n                    <tr>\r\n                        <td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 40px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n                            <p style=\"margin: 0;\">Başladığınız için heyecanlıyız. Öncelikle, hesabınızı onaylamanız gerekir. Sadece aşağıdaki düğmeye basın.</p>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td bgcolor=\"#ffffff\" align=\"left\">\r\n                            <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n                                <tr>\r\n                                    <td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 20px 30px 60px 30px;\">\r\n                                        <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n                                            <tr>\r\n                                                <td align=\"center\" style=\"border-radius: 3px;\" bgcolor=\"#007fff\"><a href='https://localhost:44382{callbackUrl}' target=\"_blank\" style=\"font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; border: 1px solid #007fff; display: inline-block;\">Hesabı Onayla</a></td>\r\n                                            </tr>\r\n                                        </table>\r\n                                    </td>\r\n                                </tr>\r\n                            </table>\r\n                        </td>\r\n                    </tr> <!-- COPY -->\r\n                    <tr>\r\n                        <td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 0px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n                        \r\n                        </td>\r\n                    </tr> <!-- COPY -->\r\n                    <tr>\r\n                        <td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n                            <p style=\"margin: 0;\"><a href=\"#\" target=\"_blank\" style=\"color: #007fff;\">https://www.metaakdeniz.com</a></p>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n                           \r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n                            <p style=\"margin: 0;\">Meta Akdeniz,<br>META Team</p>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 30px 10px 0px 10px;\">\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n                    <tr>\r\n                        <td bgcolor=\"#007fff\" align=\"center\" style=\"padding: 30px 30px 30px 30px; border-radius: 4px 4px 4px 4px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\">\r\n                            <h2 style=\"font-size: 20px; font-weight: 400; color:white; margin: 0;\">ACADEMY</h2>\r\n                            <p style=\"margin: 0;\"><a href=\"#\" target=\"_blank\" style=\"color: white;\">Yardımmı Lazım ? </a></p>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\">\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n                    <tr>\r\n                        <td bgcolor=\"#f4f4f4\" align=\"left\" style=\"padding: 0px 30px 30px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;\"> <br>\r\n                            <p style=\"margin: 0;\">Tüm Haklar Saklıdır . <a href=\"#\" target=\"_blank\" style=\"color: #111111; font-weight: 700;\">www.metaakdeniz.com</a>.</p>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</body>\r\n\r\n</html>");


                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Başarılı",
                        Message = "Lütfen Eposta adresine gelen link üzerinden epostanızı onaylayınız",
                        Css = "success"
                    });

                    return RedirectToAction("Index", "Account");
                }
            }
            return View(model);
        }

    }

}

