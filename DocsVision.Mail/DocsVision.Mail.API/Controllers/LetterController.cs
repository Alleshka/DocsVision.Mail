using System;
using System.Collections.Generic;
using System.Web.Http;
using DocsVision.Mail.Model;
using DocsVision.Mail.DataLayer;
using DocsVision.Mail.DataLayer.SQL;

namespace DocsVision.Mail.API.Controllers
{
    [CustomExceptionAtribute]
    public class LetterController : ApiController
    {
        private ILetterRepository letterRepository;
        private String _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["_connectionString"].ConnectionString;

        public LetterController()
        {
            letterRepository = new TsqlLetterRepository(_connectionString);
        }

        [HttpPost]
        [Route("api/letter")]
        public Letter CreateLetter([FromBody]Letter letter)
        {
            Letter tmp = letterRepository.CreateLetter(letter);
            return tmp;
        }

        [HttpGet]
        [Route("api/letter/{id}")]
        public Letter GetLetterInfo(Guid id)
        {
            return letterRepository.GetLetter(id);
        }

        [HttpGet]
        [Route("api/letter/{id}/read/{userID}")]
        public void ReadLetter(Guid id, Guid userId)
        {
            letterRepository.ReadLetter(id, userId);
        }

        [HttpPost]
        [Route("api/letter/{id}/send/{userID}")]
        public void SendLetter(Guid id, Guid userID)
        {
            letterRepository.SendLetter(id, userID);
        }

        [HttpGet]
        [Route("api/letter/new/{userID}")]
        public IEnumerable<Letter> GetNewLetters(Guid userID)
        {
            IEnumerable<Letter> letters = letterRepository.GetNewLetters(userID);
            return letters;
        }

        [HttpGet]
        [Route("api/letter/all/{userID}")]
        public IEnumerable<Letter> GetAllLetters(Guid userID)
        {
            return letterRepository.GetMyLetters(userID);
        }

        [HttpGet]
        [Route("api/letter/sended/{userID}")]
        public IEnumerable<Letter> GetSendedLetters(Guid userID)
        {
            return letterRepository.GetSendedLetters(userID);
        }
    }
}
