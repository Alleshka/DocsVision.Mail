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
        public Guid Sender { get; set; } 
        public DateTime CreateDate { get; set; }
        public DateTime SendDate { get; set; }
    }
}
