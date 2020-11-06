using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VariousTests.WEB.Models
{
    public class AnswerViewModel
    {
        public string Name { get; set; }
        public int QuestionId { get; set; }
        public bool Right { get; set; }
    }
}