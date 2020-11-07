﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VariousTests.WEB.Models
{
    public class QuestionViewModel
    {
        [Required]
        public string Name { get; set; }
        public int TestId { get; set; }
    }
}