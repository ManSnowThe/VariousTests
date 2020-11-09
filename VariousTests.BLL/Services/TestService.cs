using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousTests.BLL.Interfaces;
using VariousTests.DAL.Interfaces;
using VariousTests.BLL.Infrastructure;
using VariousTests.BLL.DTO;
using VariousTests.DAL.Entities;
using AutoMapper;

namespace VariousTests.BLL.Services
{
    public class TestService : ITestService
    {
        IUnitOfWork Database { get; set; }

        public TestService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<(Details details, int id)> CreateTest(TestDTO testDto)
        {
            var test = await Database.TestRepository.Get(testDto.Id);

            if (test != null)
            {
                return (details: new Details(false, "При создании теста что-то пошло не так", ""), id: 0);
            }

            VarTest varTest = new VarTest
            {
                Name = testDto.Name,
                Description = testDto.Description,
                CreationDate = DateTime.Now,
                AuthorId = testDto.AuthorId,
                TopicId = testDto.TopicId
            };

            Database.TestRepository.Create(varTest);
            await Database.SaveAsync();

            int testId = varTest.Id;

            return (details: new Details(true, "Тест создан успешно", ""), id: testId);
        }

        public async Task<(Details details, int id)> AddQuestion(QuestionDTO questionDto)
        {
            var test = await Database.TestRepository.Get(questionDto.TestId);

            if (test == null)
            {
                return (details: new Details(false, "Ошибка был при создании вопроса, повторите попытку", ""), id: 0);
            }

            VarQuestion varQuestion = new VarQuestion
            {
                Name = questionDto.Name,
                TestId = test.Id
            };

            Database.QuestionRepository.Create(varQuestion);
            await Database.SaveAsync();

            int questId = varQuestion.Id;

            return (details: new Details(true, "Вопрос добавлен", ""), id: questId);
        }

        //// Залить сразу несколько вопросов
        //public async Task<Details> AddQuestions(IEnumerable<QuestionDTO> questionDtos)
        //{
        //    var test = await Database.TestRepository.Get(questionDtos.FirstOrDefault().TestId);

        //    if (test == null)
        //    {
        //        return new Details(false, "Ошибка при создании вопроса, повторите попытку", "");
        //    }

        //    //List<VarQuestion> varQuest = new List<VarQuestion>();

        //    foreach(QuestionDTO qDto in questionDtos)
        //    {
        //        VarQuestion varQuestion = new VarQuestion
        //        {
        //            Name = qDto.Name,
        //            TestId = test.Id
        //        };
        //        //varQuest.Add(varQuestion);
        //        Database.QuestionRepository.Create(varQuestion);
        //    }

        //    //Database.QuestionRepository.Create(varQuest);
        //    await Database.SaveAsync();

        //    return new Details(true, "Вопросы добавлены", "");
        //}

        public async Task<Details> AddAnswer(AnswerDTO answerDto)
        {
            var question = await Database.QuestionRepository.Get(answerDto.QuestionId);

            if (question == null)
            {
                return new Details(false, "Ошибка при создании ответа", "");
            }

            VarAnswer varAnswer = new VarAnswer
            {
                Name = answerDto.Name,
                QuestionId = question.Id,
                Right = answerDto.Right
            };

            Database.AnswerRepository.Create(varAnswer);
            await Database.SaveAsync();

            return new Details(true, "Ответ добавлен к вопросу", "");
        }

        public IEnumerable<TopicDTO> GetTopics()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<VarTopic, TopicDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<VarTopic>, List<TopicDTO>>(Database.TopicRepository.GetAll());
        }

        public IEnumerable<TestDTO> GetTests()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<VarTest, TestDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<VarTest>, List<TestDTO>>(Database.TestRepository.GetAll());
        }

        public async Task<TestDTO> GetTest(int? id)
        {
            if (id == null)
            {
                throw new DetailsException("Не указан id теста", "");
            }

            var test = await Database.TestRepository.Get(id.Value);

            if (test == null)
            {
                throw new DetailsException("Тест не найден", "");
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<VarTest, TestDTO>()).CreateMapper();
            return mapper.Map<VarTest, TestDTO>(test);
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestions(TestDTO testDto)
        {
            var test = await Database.TestRepository.Get(testDto.Id);

            if (test == null)
            {
                throw new DetailsException("Тест не найден", "");
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<VarQuestion, QuestionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<VarQuestion>, List<QuestionDTO>>(Database.QuestionRepository.Find(question => question.TestId == test.Id));
        }

        public async Task<IEnumerable<AnswerDTO>> GetAnswers(QuestionDTO questionDto)
        {
            var question = await Database.QuestionRepository.Get(questionDto.Id);

            if (question == null)
            {
                throw new DetailsException("Вопрос не найден", "");
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<VarAnswer, AnswerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<VarAnswer>, List<AnswerDTO>>(Database.AnswerRepository.Find(answer => answer.QuestionId == question.Id));
        }

        public async Task<QuestionDTO> GetQuestion(int? id)
        {
            if (id == null)
            {
                throw new DetailsException("Не указан id вопроса", "");
            }

            var question = await Database.QuestionRepository.Get(id.Value);

            if (question == null)
            {
                throw new DetailsException("Вопрос не найден", "");
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<VarQuestion, QuestionDTO>()).CreateMapper();
            return mapper.Map<VarQuestion, QuestionDTO>(question);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
