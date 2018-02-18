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
        public String Head { get; set; } // Заголовок письма
        public DateTime SendDate { get; set; } // Дата отправки
        public Guid Sender { get; set; } // Отправитель
        public Guid Recipient { get; set; } // Принимающий
        public String ContentMessage { get; set; } // Сообщение
        public bool IsRead { get; set; }
    }
}
