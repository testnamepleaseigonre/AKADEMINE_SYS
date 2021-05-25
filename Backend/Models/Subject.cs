using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKADEMINE_SYS.Backend.Models
{
    public class Subject
    {
        public int Id { get; private set; }
        public string name { get; private set; }
        public string proffesorID { get; private set; }

        public Subject(int id, string name, string profID)
        {
            Id = id;
            this.name = name;
            this.proffesorID = profID;
        }
    }
}
