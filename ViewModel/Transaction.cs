using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class Transaction
    {
        public string Message { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
