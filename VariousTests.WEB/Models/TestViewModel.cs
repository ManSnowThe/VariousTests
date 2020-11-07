using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VariousTests.WEB.Models
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TopicId { get; set; }
        public string AuthorId { get; set; }
    }
}