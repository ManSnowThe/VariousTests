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
        IRepository<VarTest> TestRepository { get; }
        IRepository<VarTopic> TopicRepository { get; }
        IRepository<VarQuestion> QuestionRepository { get; }
        IRepository<VarAnswer> AnswerRepository { get; }

        Task SaveAsync();
    }
}
