using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Entity
{
    public class Organisation
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Partner Partner { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
