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
        Letter CreateLetter(Letter letter);
        void SendLetter(Guid letterID, Guid userID);
        void ReadLetter(Guid letterID, Guid userID);

        IEnumerable<Letter> GetNewLetters(Guid userId);
        IEnumerable<Letter> GetMyLetters(Guid userID);

        IEnumerable<Letter> GetSendedLetters(Guid userID);
    }
}
