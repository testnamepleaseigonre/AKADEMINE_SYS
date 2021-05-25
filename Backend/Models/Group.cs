using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKADEMINE_SYS.Backend.Models
{
    public class Group
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
       
        public Group(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
