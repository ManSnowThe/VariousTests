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
    public class TestRepository : IRepository<VarTest>
    {
        public ApplicationContext Database { get; set; }

        public TestRepository(ApplicationContext db)
        {
            Database = db;
        }

        public IEnumerable<VarTest> GetAll()
        {
            return Database.Tests;
        }

        public async Task<VarTest> Get(int id)
        {
            return await Database.Tests.FindAsync(id);
        }

        public void Create(VarTest test)
        {
            Database.Tests.Add(test);
        }

        public void Update(VarTest test)
        {
            Database.Entry(test).State = EntityState.Modified;
        }

        public IEnumerable<VarTest> Find(Func<VarTest, Boolean> predicate)
        {
            return Database.Tests.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            VarTest test = Database.Tests.Find(id);
            if (test != null)
            {
                Database.Tests.Remove(test);
            }
        }
    }
}
