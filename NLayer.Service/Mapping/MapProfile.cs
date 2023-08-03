using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.BranchDtos;
using NLayer.Core.DTOs.CartDtos;
using NLayer.Core.DTOs.CartItemDtos;
using NLayer.Core.DTOs.ClassBranchDtos;
using NLayer.Core.DTOs.ClassDtos;
using NLayer.Core.DTOs.ExamAnswersDtos;
using NLayer.Core.DTOs.ExamDtos;
using NLayer.Core.DTOs.ExamQuestionDtos;
using NLayer.Core.DTOs.GuardianDtos;
using NLayer.Core.DTOs.LessonDtos;
using NLayer.Core.DTOs.LevelDtos;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.DTOs.OptionDtos;
using NLayer.Core.DTOs.OutputDtos;
using NLayer.Core.DTOs.QuestionDtos;
using NLayer.Core.DTOs.ScorsDtos;
using NLayer.Core.DTOs.SolutionDtos;
using NLayer.Core.DTOs.SubjectDtos;

namespace NLayer.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //branch dtos
            CreateMap<Branch, BranchDto>().ReverseMap();
            CreateMap<Branch, BranchListDto>().ReverseMap();
            //CartDtos
            CreateMap<Cart, CartDto>().ReverseMap();
            //CartItem Dtos
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            //ClassBranch Dtos
            CreateMap<ClassBranch, ClassBranchDto>().ReverseMap();
            //Class Dtos
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Class, ClassListDto>().ReverseMap();
            //ExamAnswer Dtos
            CreateMap<ExamAnswers, ExamAnswersDto>().ReverseMap();
            //Exam Dtos
            CreateMap<Exam, ExamDto>().ReverseMap();
            //ExamQuestion Dtos
            CreateMap<ExamQuestions, ExamQuestionDto>().ReverseMap();
            //Guardian Dto 
            CreateMap<Guardian, GuardianDto>().ReverseMap();
            //Lesson Dtos
            CreateMap<Lesson, LessonDto>().ReverseMap();
            //Level Dtos
            CreateMap<Level, LevelDto>().ReverseMap();
            //Message Dtos
            CreateMap<Message, MessageDto>().ReverseMap();
            //Option Dtos
            CreateMap<Option, OptionDto>().ReverseMap();
            //Output Dtos
            CreateMap<Output, OutputDto>().ReverseMap();
            //Question Dtos
            CreateMap<Question, QuestionDto>().ReverseMap();
            //Scors Dtos
            CreateMap<Scors, ScorListDto>().ReverseMap();
            //Solution Dtos
            CreateMap<Solution, SolutionDto>().ReverseMap();
            //Subject Dtos
            CreateMap<Subject, SubjectDto>().ReverseMap();



        }
    }
}
