using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousTests.DAL.Interfaces;
using VariousTests.DAL.EF;
using VariousTests.DAL.Entities;

namespace VariousTests.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        public IRepository<VarTest> TestRepository { get; set; }
        public IRepository<VarTopic> TopicRepository { get; set; }
        public IRepository<VarQuestion> QuestionRepository { get; set; }
        public IRepository<VarAnswer> AnswerRepository { get; set; }

        public UnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);

            TestRepository = new TestRepository(db);
            TopicRepository = new TopicRepository(db);
            QuestionRepository = new QuestionRepository(db);
            AnswerRepository = new AnswerRepository(db);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
