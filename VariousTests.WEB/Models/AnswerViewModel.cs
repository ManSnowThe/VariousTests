using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VariousTests.WEB.Models
{
    public class AnswerViewModel
    {
        [Required]
        [Display(Name = "Ответ на вопрос")]
        public string Name { get; set; }
        public int QuestionId { get; set; }
        [Display(Name = "Правильный ответ?")]
        public bool Right { get; set; }
    }
}