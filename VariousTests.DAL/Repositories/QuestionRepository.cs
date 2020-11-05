using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousTests.DAL.Interfaces;
using VariousTests.DAL.Entities;
using VariousTests.DAL.EF;
using System.Data.Entity;

namespace VariousTests.DAL.Repositories
{
    public class QuestionRepository : IRepository<VarQuestion>
    {
        public ApplicationContext Database { get; set; }

        public QuestionRepository(ApplicationContext db)
        {
            Database = db;
        }

        public IEnumerable<VarQuestion> GetAll()
        {
            return Database.Questions;
        }

        public async Task<VarQuestion> Get(int id)
        {
            return await Database.Questions.FindAsync(id);
        }

        public void Create(VarQuestion question)
        {
            Database.Questions.Add(question);
        }

        public void Update(VarQuestion question)
        {
            Database.Entry(question).State = EntityState.Modified;
        }

        public IEnumerable<VarQuestion> Find(Func<VarQuestion, Boolean> predicate)
        {
            return Database.Questions.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            VarQuestion question = Database.Questions.Find(id);
            if (question != null)
            {
                Database.Questions.Remove(question);
            }
        }
    }
}
