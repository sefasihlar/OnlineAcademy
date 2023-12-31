﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamDtos;
using NLayer.Core.DTOs.QuestionDtos;
using NLayer.Core.DTOs.ScorsDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.Controllers
{
    public class MyExamController : Controller
    {
        private readonly IExamService _examService;
        private readonly IExamAnswersService _answerService;
        private readonly IScorsService _scorsService;
        private readonly IAppUserService _appUserService;
        private readonly IQuestionService _questionService;
        private readonly ICartItemService _cartItemService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public MyExamController(IExamService examService, IExamAnswersService answerService, IScorsService scorsService, IAppUserService appUserService, IQuestionService questionService, ICartItemService cartItemService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _examService=examService;
            _answerService=answerService;
            _scorsService=scorsService;
            _appUserService=appUserService;
            _questionService=questionService;
            _cartItemService=cartItemService;
            _userManager=userManager;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index()
        {

            var ExamAnswerList = await _answerService.GetAllAsycn();

            var CartItemList = await _cartItemService.GetAllAsycn();

            var ExamList = await _examService.GetWithList();

            var ExamListEntity = _mapper.Map<List<Exam>>(ExamList);

            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            var getId = await _appUserService.GetByIdAsycn(Convert.ToInt32(userId));

            var answeredExamIds = ExamAnswerList.Select(a => a.ExamId).ToList();

            //kuyllancıyla birlikte  sınavın filterelemesi için gerekli alan Cart ve Cart Item
            var cartItemedExamIds = CartItemList.Select(x => x.ExamId).ToList();

            var values = new ExamListDto()
            {
                Exams =ExamListEntity
                //buradaya cart item içersinde tutulan examId var ise kullanıcıya gösterilmesin olacak
                .Where(x => x.ClassId == getId.ClassId /*&& !answeredExamIds.Contains(x.Id)*/)
                .ToList()
            };
            return View(values);
        }

        public async Task<IActionResult> TeacherExams()
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            var getId =await _appUserService.GetByIdAsycn(Convert.ToInt32(userId));

            var ExamList = _mapper.Map<List<Exam>>(await _examService.GetWithList());
            var ScorList =_mapper.Map<List<ScorListDto>>(await _scorsService.GetAllAsycn());

            var values = new ExamListDto()
            {
                Exams = ExamList.Where(x => x.UserId == getId.Id).ToList(),
                Scors = ScorList.Where(x => x.UserId == getId.Id).ToList(),
            };

            List<int> examIds = new List<int>();

            foreach (var item in values.Exams)
            {
                foreach (var value in values.Scors)
                {
                    if (item.Id == value.ExamId)
                    {
                        examIds.Add(value.Id);
                    }
                }
            }

            ViewBag.Scors = ScorList;

            return View(values);
        }

        public async Task<IActionResult> Exam(int id)
        {
            var süre = 60;

            var valuesList = _mapper.Map<List<Question>>(_questionService.GetQuestionsByExamList(id));

            var values = new QuestionListDto()
            {
                Questions =valuesList,

                SureDegeri = süre
            };

            ViewBag.ExamId = id;

            return View(values);
        }
    }
}
