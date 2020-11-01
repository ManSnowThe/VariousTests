using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariousTests.DAL.Entities
{
    public class VarAnswer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Right { get; set; }

        public int QuestionId { get; set; }
        public VarQuestion Question { get; set; }
    }
}
