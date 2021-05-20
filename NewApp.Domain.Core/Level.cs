using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewApp.Domain.Core
{
    public partial class Level
    {
        public Level()
        {
            Mentees = new HashSet<Mentee>();
        }
        public int LevelId { get; set; }
        public string Position { get; set; }

        public virtual ICollection<Mentee> Mentees { get; set; }
    }
}
