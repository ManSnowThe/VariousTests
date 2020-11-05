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
    public class TopicRepository : IRepository<VarTopic>
    {
        public ApplicationContext Database { get; set; }

        public TopicRepository(ApplicationContext db)
        {
            Database = db;
        }

        public IEnumerable<VarTopic> GetAll()
        {
            return Database.Topics;
        }

        public async Task<VarTopic> Get(int id)
        {
            return await Database.Topics.FindAsync(id);
        }

        public void Create(VarTopic topic)
        {
            Database.Topics.Add(topic);
        }

        public void Update(VarTopic topic)
        {
            Database.Entry(topic).State = EntityState.Modified;
        }

        public IEnumerable<VarTopic> Find(Func<VarTopic, Boolean> predicate)
        {
            return Database.Topics.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            VarTopic topic = Database.Topics.Find(id);
            if (topic != null)
            {
                Database.Topics.Remove(topic);
            }
        }
    }
}
