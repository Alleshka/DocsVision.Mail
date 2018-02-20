using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocsVision.Mail.DataLayer;
using DocsVision.Mail.Model;
using System.Data.SqlClient;

namespace DocsVision.Mail.DataLayer.SQL
{
    public class TsqlLetterRepository : ILetterRepository
    {
        private String _connectionString;
        private IUserRepository userRepository;

        public TsqlLetterRepository(String connectString, IUserRepository repository)
        {
            _connectionString = connectString;
            userRepository = repository;
        }

        public Letter CreateLetter(Letter letter)
        {
            letter.Id = Guid.NewGuid();

            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "CreateLetter";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", letter.Id);
                    command.Parameters.AddWithValue("@head", letter.Head);
                    command.Parameters.AddWithValue("@contentmessage", letter.ContentMessage);
                    command.Parameters.AddWithValue("@sender", letter.Sender.Id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read()) throw new Exception("Создание письма не удалось");
                        else return ParseLetter(reader);
                    }
                }
            }
        }

        public Letter GetLetter(Guid id)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "GetLetterInfo";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@letterId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read()) throw new Exception("Создание письма не удалось");
                        else return ParseLetter(reader);
                    }
                }
            }
        }

        public IEnumerable<Letter> GetMyLetters(Guid userID)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "GetAllLetters";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@userID", userID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Letter letter = ParseLetter(reader);
                            letter.Recepient = userRepository.GetUserInfo(userID);
                            letter.SendDate = reader.GetDateTime(reader.GetOrdinal("senddate"));
                            letter.IsRead = reader.GetBoolean(reader.GetOrdinal("IsRead"));
                            yield return letter;
                        }
                    }
                }
            }
        }
        public IEnumerable<Letter> GetNewLetters(Guid userId)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "GetNewLetters";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@userID", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Letter letter = ParseLetter(reader);
                            letter.Recepient = userRepository.GetUserInfo(userId);
                            letter.SendDate = reader.GetDateTime(reader.GetOrdinal("senddate"));
                            letter.IsRead = false;
                            //letter.IsRead = reader.GetBoolean(reader.GetOrdinal("IsRead"));
                            yield return letter;
                        }
                    }
                }
            }
        }

        public IEnumerable<Letter> GetSendedLetters(Guid userID)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "GetSendedLetters";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@userID", userID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Letter letter = ParseLetter(reader);
                            letter.Recepient = userRepository.GetUserInfo(reader.GetGuid(reader.GetOrdinal("rec")));
                            letter.SendDate = reader.GetDateTime(reader.GetOrdinal("senddate"));
                            yield return letter;
                        }
                    }
                }
            }
        }

        public void ReadLetter(Guid letterID, Guid userID)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "ReadLetter";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@letterID", letterID);
                    command.Parameters.AddWithValue("@userID", userID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void SendLetter(Guid letterID, Guid userID)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                sql.Open();

                using (var command = sql.CreateCommand())
                {
                    command.CommandText = "SendLetter";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@letterID", letterID);
                    command.Parameters.AddWithValue("@empoyeeID", userID);

                    command.ExecuteNonQuery();
                }
            }
        }

        private Letter ParseLetter(SqlDataReader reader)
        {
            Letter letter = new Letter()
            {
                Id = reader.GetGuid(reader.GetOrdinal("id")),
                Head = reader.GetString(reader.GetOrdinal("head")),
                ContentMessage = reader.GetString(reader.GetOrdinal("contentmessage")),
                Sender = userRepository.GetUserInfo(reader.GetGuid(reader.GetOrdinal("sender"))),
            };

            return letter;
        }
    }
}
