using System;
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

                Details details = await TestService.CreateTest(testDto);
                if (details.Succeeded)
                {
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError(details.Property, details.Message);
                }
            }
            return View(model);
        }

        public ActionResult AddQuestion()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddQuestion(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionDto = new QuestionDTO { Name = model.Name, TestId = model.TestId };

                Details details = await TestService.AddQuestion(questionDto);
                if (details.Succeeded)
                {
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError(details.Property, details.Message);
                }
            }
            return View(model);
        }

        public ActionResult AddAnswer()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAnswer(AnswerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var answerDto = new AnswerDTO { Name = model.Name, QuestionId = model.QuestionId, Right = model.Right };

                Details details = await TestService.AddAnswer(answerDto);
                if (details.Succeeded)
                {
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError(details.Property, details.Message);
                }
            }
            return View(model);
        }
    }
}