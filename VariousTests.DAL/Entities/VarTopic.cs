using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariousTests.DAL.Entities
{
    public class VarTopic
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<VarTest> Tests { get; set; }
    }
}
