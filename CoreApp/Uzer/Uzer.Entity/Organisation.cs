using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uzer.Entity
{
    public class Organisation
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Partner Partner { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
