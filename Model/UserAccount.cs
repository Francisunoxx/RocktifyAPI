using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserAccount : Common
    {
        [Key]
        public int UserAccountId { get; set; }
        public string Password { get; set; }
        public virtual User User { get; set; }
    }
}
