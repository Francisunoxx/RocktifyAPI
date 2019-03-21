using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User : Common
    {
        [Key]
        public int UserId { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
