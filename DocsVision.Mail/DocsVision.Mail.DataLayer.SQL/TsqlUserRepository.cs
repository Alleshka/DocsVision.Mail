﻿using System;
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
                        if (!reader.Read()) return null;
                        else return ParseEmployee(reader);
                    }
                }
            }
        }

        public Employee FindUserByLogin(string login)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "FindUserByLogin";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@login", login);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read()) return null;
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
                        if (!reader.Read()) return null;
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

                    String passHash = GetPassHash(password);

                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", passHash);

                    using (var reader = command.ExecuteReader())
                    {   
                        if (!reader.Read()) return null;
                        else return ParseEmployee(reader);
                    }
                }
            }
        }

        public void FullDeleteUser(Guid id)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "delete from Employee where id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        private String GetPassHash(String password)
        {
            var data = System.Text.Encoding.ASCII.GetBytes(password);

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(data);

            String result = "";

            // Чтобы была норм строка, иначе крокозябры
            foreach (var b in md5data)
            {
                result += b.ToString("x2");
            }

            return result;
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
