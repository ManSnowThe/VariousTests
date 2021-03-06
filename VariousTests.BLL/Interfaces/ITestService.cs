﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousTests.BLL.Infrastructure;
using VariousTests.BLL.DTO;

namespace VariousTests.BLL.Interfaces
{
    public interface ITestService : IDisposable
    {
        Task<(Details details, int id)> CreateTest(TestDTO testDto);
        IEnumerable<TopicDTO> GetTopics(); 
        Task<(Details details, int id)> AddQuestion(QuestionDTO questionDto);
        Task<Details> AddAnswer(AnswerDTO answerDTO);
        IEnumerable<TestDTO> GetTests();
        Task<TestDTO> GetTest(int? id);
        Task<IEnumerable<QuestionDTO>> GetQuestions(TestDTO testDto);
        Task<IEnumerable<AnswerDTO>> GetAnswers(QuestionDTO questionDto);
        Task<QuestionDTO> GetQuestion(int? id);
        //Task<Details> AddQuestions(IEnumerable<QuestionDTO> questionDtos);
    }
}
