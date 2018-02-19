using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocsVision.Mail.DataLayer.SQL;
using DocsVision.Mail.Model;

namespace DocsVision.Mail.DataLayer.Sql.Test
{
    [TestClass]
    public class UserRepTest
    {
        private String _connectionString = "Data Source=DESKTOP-H4JQP0V;Initial Catalog=DVTestMail;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private List<Guid> createdList = new List<Guid>();

        [TestMethod]
        public void GetUserInfo()
        {
            // Arrange 
            Guid.TryParse("0f8fad5b-d9cb-469f-a165-70867728950e", out Guid testGuid);

            Employee employee = new Employee()
            {
                FirstName = "Alleshka",
                LastName = "Alleshka",
                Login = "Alleshka",
                Id = testGuid
            };

            // Act
            TsqlUserRepository userRepository = new TsqlUserRepository(_connectionString);
            Employee user = userRepository.GetUserInfo(testGuid);

            // Asserts
            Assert.AreEqual(testGuid, user.Id);
            Assert.AreEqual(employee.FirstName, user.FirstName);
            Assert.AreEqual(employee.LastName, user.LastName);
            Assert.AreEqual(employee.Login, user.Login);
        }

        [TestMethod]
        public void CreateUser()
        {
            // Arrange
            Employee emp = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = "Alleshka",
                LastName = "Alleshka",
                Login = Guid.NewGuid().ToString(),
                Password = "123qwe"
            };

            // Act
            TsqlUserRepository userRepository = new TsqlUserRepository(_connectionString);
            Employee user = userRepository.CreateEmployee(emp);
            createdList.Add(user.Id);

            // Asserts
            Assert.AreEqual(emp.Id, user.Id);
            Assert.AreEqual(emp.FirstName, user.FirstName);
            Assert.AreEqual(emp.LastName, user.LastName);
            Assert.AreEqual(emp.Login, user.Login);
        }

        [TestMethod]
        public void FindUserByLogin()
        {
            // Arrange
            Guid.TryParse("0f8fad5b-d9cb-469f-a165-70867728950e", out Guid testGuid);
            Employee emp = new Employee()
            {
                FirstName = "Alleshka",
                LastName = "Alleshka",
                Login = "Alleshka",
                Id = testGuid
            };

            // Act
            TsqlUserRepository userRepository = new TsqlUserRepository(_connectionString);
            Employee user = userRepository.FindUserByLogin(emp.Login);

            // Asserts
            Assert.AreEqual(emp.Id, user.Id);
            Assert.AreEqual(emp.FirstName, user.FirstName);
            Assert.AreEqual(emp.LastName, user.LastName);
            Assert.AreEqual(emp.Login, user.Login);
        }

        [TestMethod]
        public void LoginUserAccept()
        {
            // Assert
            String login = "Alleshka";
            String password = "123qwe";

            // Act
            TsqlUserRepository userRepository = new TsqlUserRepository(_connectionString);
            var user = userRepository.LoginEmployee(login, password); // Вход

            // Asserts
            Assert.AreEqual(login, user.Login);
        }

        [TestMethod]
        public void LoginFalledL()
        {
            // Assert
            String login = "Alleshka3";
            String password = "123qwe";
            bool flag = false;

            // Act
            TsqlUserRepository userRepository = new TsqlUserRepository(_connectionString);

            try
            {
                var user = userRepository.LoginEmployee(login, password);
                flag = false;
            }
            catch (Exception ex)
            {
                flag = true;
            }

            // Assert
            Assert.AreEqual(true, flag); // Если исключение сработало - значит ок
        }

        [TestMethod]
        public void LoginFalledP()
        {
            // Assert
            String login = "Alleshka";
            String password = "123qwe2";
            bool flag = false;

            // Act
            TsqlUserRepository userRepository = new TsqlUserRepository(_connectionString);

            try
            {
                var user = userRepository.LoginEmployee(login, password);
                flag = false;
            }
            catch (Exception ex)
            {
                flag = true;
            }

            // Assert
            Assert.AreEqual(true, flag); // Если исключение сработало - значит ок
        }



        [TestCleanup]
        public void Clean()
        {
            TsqlUserRepository userRepository = new TsqlUserRepository(_connectionString);

            foreach (Guid id in createdList)
            {
                userRepository.FullDeleteUser(id);
            }
        }
    }
}
