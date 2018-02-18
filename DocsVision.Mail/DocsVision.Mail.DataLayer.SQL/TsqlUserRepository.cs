using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocsVision.Mail.DataLayer;
using DocsVision.Mail.Model;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace DocsVision.Mail.DataLayer.SQL
{
    public class TsqlUserRepository : IUserRepository
    {

        private String _connectionString;

        public TsqlUserRepository(String connectString)
        {
            _connectionString = connectString;
        }

        public Employee CreateEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            employee.Password = GetPassHash(employee.Password);

            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "CreateUser";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", employee.Id);
                    command.Parameters.AddWithValue("@Login", employee.Login);
                    command.Parameters.AddWithValue("@Password", employee.Password);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read()) throw new Exception("Создание пользователя не удалось");
                        else return ParseEmployee(reader);
                    }
                }
            }
        }

        public Employee GetUserInfo(Guid id)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "GetUserInfo";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read()) throw new Exception("Пользователь не найден");
                        else return ParseEmployee(reader);
                    }
                }
            }
        }

        public Employee LoginEmployee(string login, string password)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "SignIn";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", GetPassHash(password));

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read()) throw new Exception("Неверный логин или пароль");
                        else return ParseEmployee(reader);
                    }
                }
            }
        }

        private String GetPassHash(String password)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(password);

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(data);

            var hashdata = System.Text.Encoding.UTF8.GetString(md5data);
            return hashdata;
        }
        private Employee ParseEmployee(SqlDataReader reader)
        {
            return new Employee()
            {
                Id = reader.GetGuid(reader.GetOrdinal("id")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                Login = reader.GetString(reader.GetOrdinal("Login"))
            };
        }
    }
}
