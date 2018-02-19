using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocsVision.Mail.Model;

namespace DocsVision.Mail.DataLayer
{
    public interface IUserRepository
    {
        Employee CreateEmployee(Employee employee); // Создание пользователя
        Employee LoginEmployee(String login, String password); // Вход пользователя
        Employee GetUserInfo(Guid id); // Получить информацию о пользователе
        Employee FindUserByLogin(String login); // Найти пользователя по логину
    }
}
