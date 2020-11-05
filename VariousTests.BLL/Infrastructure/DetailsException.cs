using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariousTests.BLL.Infrastructure
{
    public class DetailsException : Exception
    {
        public string Property { get; protected set; }
        public DetailsException(string message, string property) : base(message)
        {
            Property = property;
        }
    }
}
