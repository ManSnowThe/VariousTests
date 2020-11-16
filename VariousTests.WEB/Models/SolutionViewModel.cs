using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VariousTests.WEB.Models
{
    public class SolutionViewModel
    {
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}