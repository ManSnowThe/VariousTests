using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariousTests.BLL.Infrastructure
{
    public class Details
    {
        public bool Succeeded { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }

        public Details(bool succeeded, string message, string property)
        {
            Succeeded = succeeded;
            Message = message;
            Property = property;
        }
    }
}
