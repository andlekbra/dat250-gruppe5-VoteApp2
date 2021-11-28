﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Application.Features.PollQuestions.Commands.Add;
using VoteApp.Application.Features.PollQuestions.Queries.GetAll;
using VoteApp.Domain.Entities;

namespace VoteApp.Application.Mappings
{
    public class PollQuestionProfile : Profile
    {
        public PollQuestionProfile()
        {
            CreateMap<AddPollQuestionCommand, PollQuestion>().ReverseMap();
            CreateMap<GetAllPollQuestionsResponse, PollQuestion>().ReverseMap();
        }
    }
}
