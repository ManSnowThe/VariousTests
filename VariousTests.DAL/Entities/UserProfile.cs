using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VariousTests.DAL.Entities
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string UserName { get; set; }

        public virtual AppUser ApplicationUser { get; set; }

        // Один ко многим + многие ко многим
        public ICollection<VarTest> Tests { get; set; }
    }
}
