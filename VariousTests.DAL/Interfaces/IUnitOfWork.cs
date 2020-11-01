using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousTests.DAL.Entities;

namespace VariousTests.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<VarTest> Tests { get; }
        IRepository<VarTopic> Topics { get; }
        IRepository<VarQuestion> Questions { get; }
        IRepository<VarAnswer> Answers { get; }

        Task SaveAsync();
    }
}
