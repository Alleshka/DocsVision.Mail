using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocsVision.Mail.Model;

namespace DocsVision.Mail.DataLayer
{
    public interface ILetterRepository
    {
        // Отправить сообщение
        void SendLetter(Letter letter);

        // Все сообщения для меня
        IEnumerable<Letter> AllLetterForMe(Guid userID);
        // Новые сообщения для меня
        IEnumerable<Letter> NewLetterForMe(Guid userID);

        // Все письма от меня
        IEnumerable<Letter> AllLetterFromMe(Guid userID);

        Letter GetLetterInfo(Guid letterID);
    }
}
