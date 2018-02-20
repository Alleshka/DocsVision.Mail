using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsVision.Mail.Model
{
    public class Letter
    {
        public Guid Id { get; set; } 
        public String Head { get; set; }
        public String ContentMessage { get; set; }
        public Employee Sender { get; set; } 
        public Employee Recepient { get; set; }
        // public DateTime CreateDate { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
