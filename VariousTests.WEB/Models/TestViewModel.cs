using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VariousTests.WEB.Models
{
    public class TestViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название теста")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Тема теста")]
        public int TopicId { get; set; }
        public string AuthorId { get; set; }
    }
}