using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VariousTests.WEB.Models
{
    public class QuestionViewModel
    {
        [Required]
        [Display(Name = "Ваш вопрос")]
        public string Name { get; set; }
        public int TestId { get; set; }
    }
}