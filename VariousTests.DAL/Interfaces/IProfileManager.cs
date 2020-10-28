using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousTests.DAL.Entities;

namespace VariousTests.DAL.Interfaces
{
    public interface IProfileManager : IDisposable
    {
        void Create(UserProfile item);
    }
}
