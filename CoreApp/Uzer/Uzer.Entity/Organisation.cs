using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uzer.Entity
{
    public class Organisation
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public long PartnerId { get; set; }
        public Partner Partner { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
