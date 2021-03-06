﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VariousTests.BLL.Interfaces;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using VariousTests.BLL.DTO;
using VariousTests.WEB.Models;
using VariousTests.BLL.Infrastructure;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace VariousTests.WEB.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private ITestService TestService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ITestService>();
            }
        }

        public ActionResult Index()
        {
            IEnumerable<TestDTO> testDtos = TestService.GetTests();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TestDTO, TestViewModel>()).CreateMapper();
            var tests = mapper.Map<IEnumerable<TestDTO>, List<TestViewModel>>(testDtos);
            return View(tests);
        }

        public ActionResult CreateTest()
        {
            SelectList topics = new SelectList(TestService.GetTopics(), "Id", "Name");
            //ViewData.Add("DropDownTopics", topics);
            ViewBag.Topics = topics;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTest(TestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TestViewModel, TestDTO>()
                .ForMember("AuthorId", opt => opt.MapFrom(src => User.Identity.GetUserId()))).CreateMapper();
                var testDto = mapper.Map<TestViewModel, TestDTO>(model);

                var tuple = await TestService.CreateTest(testDto);
                if (tuple.details.Succeeded)
                {
                    return RedirectToAction("AddQuestion", "Test", new { id = tuple.id });
                }
                else
                {
                    ModelState.AddModelError(tuple.details.Property, tuple.details.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> AddQuestion(int? id)
        {
            try
            {
                TestDTO testDto = await TestService.GetTest(id);
                var question = new QuestionViewModel { TestId = testDto.Id };

                return View(question);

                //TestDTO testDto = await TestService.GetTest(id);
                //ViewBag.TestId = testDto.Id;
                //return View();
            }
            catch (DetailsException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddQuestion(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionDto = new QuestionDTO { Name = model.Name, TestId = model.TestId };
                var tuple = await TestService.AddQuestion(questionDto);
                if (tuple.details.Succeeded)
                {
                    //return View("Index");
                    return RedirectToAction("AddAnswer", "Test", new { id = tuple.id });
                }
                else
                {
                    ModelState.AddModelError(tuple.details.Property, tuple.details.Message);
                }
            }
            return View(model);
        }

        //[HttpGet]
        //public async Task<ActionResult> AddQuestionPartial(int? id)
        //{
        //    try
        //    {
        //        TestDTO testDto = await TestService.GetTest(id);
        //        var question = new QuestionViewModel { TestId = testDto.Id };

        //        return PartialView(question);
        //    }
        //    catch (DetailsException ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AddQuestions(IEnumerable<QuestionViewModel> models)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<QuestionViewModel, QuestionDTO>()).CreateMapper();
        //        var questionDtos = mapper.Map<IEnumerable<QuestionViewModel>, IEnumerable<QuestionDTO>>(models);

        //        Details details = await TestService.AddQuestions(questionDtos);
        //        if (details.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Test");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(details.Property, details.Message);
        //        }
        //    }
        //    return View(models);
        //}

        [HttpGet]
        public async Task<ActionResult> AddAnswer(int? id)
        {
            try
            {
                QuestionDTO questDto = await TestService.GetQuestion(id);
                var answer = new AnswerViewModel { QuestionId = questDto.Id };

                ViewBag.TestId = questDto.TestId;
                return View(answer);
            }
            catch (DetailsException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddAnswer(AnswerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var answerDto = new AnswerDTO { Name = model.Name, QuestionId = model.QuestionId, Right = model.Right };

                Details details = await TestService.AddAnswer(answerDto);
                if (details.Succeeded)
                {
                    //return RedirectToAction("AddAnswer", "Test");
                    //return View(model);
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.AddModelError(details.Property, details.Message);
                }
            }
            //return View(model);
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<ActionResult> Tests(int? id)
        {
            try
            {
                TestDTO testDto = await TestService.GetTest(id);
                ViewBag.Name = testDto.Name;

                IEnumerable<QuestionDTO> questionDtos = await TestService.GetQuestions(testDto);
                var questMapper = new MapperConfiguration(cfg => cfg.CreateMap<QuestionDTO, QuestionViewModel>()).CreateMapper();
                var quest = questMapper.Map<IEnumerable<QuestionDTO>, List<QuestionViewModel>>(questionDtos);

                IEnumerable<AnswerDTO> answerDtos = new List<AnswerDTO>();
                foreach(var question in questionDtos)
                {
                    var answers = await TestService.GetAnswers(question);
                    answerDtos = answerDtos.Concat(answers);
                }

                var answerMapper = new MapperConfiguration(cfg => cfg.CreateMap<AnswerDTO, AnswerViewModel>()).CreateMapper();
                var answer = answerMapper.Map<IEnumerable<AnswerDTO>, List<AnswerViewModel>>(answerDtos);

                var model = new SolutionViewModel
                {
                    Questions = quest,
                    Answers = answer
                };

                return View(model);
            }
            catch(DetailsException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}