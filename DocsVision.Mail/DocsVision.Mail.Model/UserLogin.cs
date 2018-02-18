using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsVision.Mail.Model
{
    public class UserLogin
    {
        public Guid Id { get; set; }
        public String userLogin { get; set; }
        public String userPassword { get; set; }
    }
}
