using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DocsVision.Mail.Model
{
    public class Employee
    {
        public Guid Id { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; } 
    }
}