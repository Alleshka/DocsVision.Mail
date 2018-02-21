using System;
using System.Web.Http;
using DocsVision.Mail.Model;
using DocsVision.Mail.DataLayer;
using DocsVision.Mail.DataLayer.SQL;

namespace DocsVision.Mail.API.Controllers
{
    [CustomExceptionAtribute]
    public class UserController : ApiController
    {
        private IUserRepository userRepository;
        private String _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["_connectionString"].ConnectionString;

        public UserController()
        {
            userRepository = new TsqlUserRepository(_connectionString);
        }

        [HttpPost]
        [Route("api/user/reg")]
        public Employee UserReg([FromBody]Employee user)
        {
            if (!ModelState.IsValid)
            {
                // TODO: Сообщение об ошибке
                return null;
            }
            else
            {
                Employee tmp = userRepository.CreateEmployee(user);
                if (tmp == null) throw new Exception("Создание пользователя не удалось");
                return tmp;
            }
        }

        [HttpPost]
        [Route("api/user/login")]
        public Employee UserLogIn([FromBody]Employee user)
        {
            Employee tmp = userRepository.LoginEmployee(user.Login, user.Password);
            if (tmp == null) throw new FalledLogin();
            return tmp;
        }

        [HttpGet]
        [Route("api/user/{id}")]
        public Employee GetInfo(Guid id)
        {
            Employee tmp = userRepository.GetUserInfo(id);
            if (tmp == null) throw new NotFoundException("Пользователь не найден");
            return tmp;
        }

        [HttpGet]
        [Route("api/user/name/{login}")]
        public Employee GetInfo(String login)
        {
            Employee tmp = userRepository.FindUserByLogin(login);
            if (tmp == null) throw new NotFoundException("Пользователь не найден");
            return tmp;
        }
    }
}
