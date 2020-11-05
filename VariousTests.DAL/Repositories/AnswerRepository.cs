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
    public class AnswerRepository : IRepository<VarAnswer>
    {
        public ApplicationContext Database { get; set; }

        public AnswerRepository(ApplicationContext db)
        {
            Database = db;
        }

        public IEnumerable<VarAnswer> GetAll()
        {
            return Database.Answers;
        }

        public async Task<VarAnswer> Get(int id)
        {
            return await Database.Answers.FindAsync(id);
        }

        public void Create(VarAnswer answer)
        {
            Database.Answers.Add(answer);
        }

        public void Update(VarAnswer answer)
        {
            Database.Entry(answer).State = EntityState.Modified;
        }

        public IEnumerable<VarAnswer> Find(Func<VarAnswer, Boolean> predicate)
        {
            return Database.Answers.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            VarAnswer answer = Database.Answers.Find(id);
            if (answer != null)
            {
                Database.Answers.Remove(answer);
            }
        }
    }
}
