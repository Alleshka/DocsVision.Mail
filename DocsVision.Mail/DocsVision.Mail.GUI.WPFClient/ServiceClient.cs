using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocsVision.Mail.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DocsVision.Mail.GUI.WPFClient
{
    public class ServiceClient
    {
        private HttpClient _client;

        public ServiceClient(String domain)
        {
            _client = new HttpClient()
            {
                BaseAddress = new System.Uri(domain)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private T ResponseParse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<T>().Result; // Возвращаем Содержимое            
            }
            else throw new Exception(response.Content.ReadAsStringAsync().Result); // Кидаем ошибку
        }

        public Employee GetUserInfo(Guid id)
        {
            var response = _client.GetAsync($"api/user/{id}").Result;
            return ResponseParse<Employee>(response);
        }

        public Employee CreateEmoloyee(Employee emp)
        {
            var response = _client.PostAsJsonAsync<Employee>($"api/user/reg", emp).Result;
            return ResponseParse<Employee>(response);
        }

        public Employee Login(String login, String password)
        {
            var response = _client.PostAsJsonAsync<Employee>($"api/user/login", new Employee() { Login = login, Password = password }).Result;
            return ResponseParse<Employee>(response);
        }

        /// <summary>
        /// Получить новые сообщения пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Letter> GetNewLetters(Guid id)
        {
            var response = _client.GetAsync($"api/letter/new/{id}").Result;
            List<Letter> letters = ResponseParse<IEnumerable<Letter>>(response).OrderBy(x => x.SendDate).ToList();
            return letters;
        }

        public List<Letter> GetAllLetters(Guid id)
        {
            var response = _client.GetAsync($"api/letter/all/{id}").Result;
            List<Letter> letters = ResponseParse<IEnumerable<Letter>>(response).OrderBy(x => x.SendDate).ToList();
            return letters;
        }

        public List<Letter> GetSendendLetters(Guid id)
        {
            var response = _client.GetAsync($"api/letter/sended/{id}").Result;
            List<Letter> letters = ResponseParse<IEnumerable<Letter>>(response).OrderBy(x => x.SendDate).ToList();
            return letters;
        }

        public void ReadLetter(Guid letterID, Guid userID)
        {
            var response = _client.GetAsync($"api/letter/{letterID}/read/{userID}").Result;
            ResponseParse<String>(response);
        }

        public void SendLetter(Letter letter, List<Guid> users)
        {
            // Создаём письмо
            var response = _client.PutAsJsonAsync("api/letter", letter).Result;
            Letter send = ResponseParse<Letter>(response);

            // Отправляем сообщения получателям
            foreach (var us in users)
            {
                var resp = _client.PostAsJsonAsync<String>($"api/letter/{send.Id}/send/{us}", null).Result;
                ResponseParse<String>(resp);
            }
        }
    }
}
