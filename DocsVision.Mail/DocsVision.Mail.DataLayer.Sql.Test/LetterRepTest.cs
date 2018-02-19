using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocsVision.Mail.DataLayer.SQL;
using DocsVision.Mail.Model;
using System.Linq;

namespace DocsVision.Mail.DataLayer.Sql.Test
{   
    [TestClass]
    public class LetterRepTest
    {
        private String _connectionString = "Data Source=DESKTOP-H4JQP0V;Initial Catalog=DVTestMail;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private List<Guid> createdList = new List<Guid>();

        [TestMethod]
        public void SendLetter()
        {
            // Arrange
            TsqlUserRepository userRepository = new TsqlUserRepository(_connectionString);
            var user1 = userRepository.FindUserByLogin("Alleshka");
            var user2 = userRepository.FindUserByLogin("Alleshka2");

            Letter letter = new Letter()
            {
                Head = "Тестовое письмо",
                ContentMessage = "Тестовое содержание",
                Sender = user1.Id
            };

            // Act
            TsqlLetterRepository tsqlLetter = new TsqlLetterRepository(_connectionString);
            letter = tsqlLetter.CreateLetter(letter);
            tsqlLetter.SendLetter(letter.Id, user2.Id);

            System.Threading.Thread.Sleep(500);

            int countLetters1 = tsqlLetter.GetNewLetters(user2.Id).ToList().Count;
            tsqlLetter.ReadLetter(letter.Id, user2.Id); // Читаем сообщение
            int countLetters2 = tsqlLetter.GetNewLetters(user2.Id).ToList().Count;

            // Asserts
            Assert.AreEqual(1, countLetters1);
            Assert.AreEqual(0, countLetters2);
        }
    }
}
