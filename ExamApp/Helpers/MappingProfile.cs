﻿using AutoMapper;
using ExamApp.DTOs.Answer;
using ExamApp.DTOs.Choice;
using ExamApp.DTOs.Exam;
using ExamApp.DTOs.Question;
using ExamApp.DTOs.User;
using ExamApp.Models;

namespace ExamApp.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exam, ExamDto>();
            CreateMap<CreateExamDto, Exam>();
            CreateMap<ExamDto, Exam>();


            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserResponseDto>();


            CreateMap<CreateQuestionDto, Question>();
            CreateMap<Question, QuestionDto>();
            
            CreateMap<Choice, ChoiceDto>().ReverseMap();
            CreateMap<Answer, AnswerDto>().ReverseMap();

            
        }
    }
}
