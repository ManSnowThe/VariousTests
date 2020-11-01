using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariousTests.DAL.Entities
{
    public class VarTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public int TopicId { get; set; }
        public VarTopic Topic { get; set; }

        public string AuthorId { get; set; }
        public UserProfile UserProfile { get; set; }

        public ICollection<VarQuestion> Questions { get; set; }

        // Многие ко многим
        public ICollection<UserProfile> UserProfiles { get; set; }
    }
}
