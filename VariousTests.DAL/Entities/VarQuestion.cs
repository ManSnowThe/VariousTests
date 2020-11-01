using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariousTests.DAL.Entities
{
    public class VarQuestion
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TestId { get; set; }
        public VarTest Test { get; set; }

        public ICollection<VarAnswer> Answers { get; set; }
    }
}
