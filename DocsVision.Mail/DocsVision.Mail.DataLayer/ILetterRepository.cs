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
        /// <summary>
        /// Создать письмо
        /// </summary>
        /// <param name="letter">Объект письма</param>
        /// <returns></returns>
        Letter CreateLetter(Letter letter);

        /// <summary>
        /// Отправить письмо
        /// </summary>
        /// <param name="letterID">ID письма</param>
        /// <param name="userID">ID пользователя</param>
        void SendLetter(Guid letterID, Guid userID);

        /// <summary>
        /// Прочитаь письмо
        /// </summary>
        /// <param name="letterID">ID письма</param>
        /// <param name="userID">ID пользователя</param>
        void ReadLetter(Guid letterID, Guid userID);

        /// <summary>
        /// Получить данные письма
        /// </summary>
        /// <returns></returns>
        Letter GetLetter(Guid id);

        /// <summary>
        /// Получить новые письма
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns></returns>
        IEnumerable<Letter> GetNewLetters(Guid userId);

        /// <summary>
        /// Получить все письма
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        IEnumerable<Letter> GetMyLetters(Guid userID);

        /// <summary>
        /// Получить отправленные письма
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        IEnumerable<Letter> GetSendedLetters(Guid userID);
    }
}
