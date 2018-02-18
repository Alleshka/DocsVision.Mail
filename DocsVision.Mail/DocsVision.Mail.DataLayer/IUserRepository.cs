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
        // Создать нового пользователя
        void CreateUser(UserLogin login, UserInfo info = null);

        UserInfo GetUserInfo(Guid id);
    }
}
