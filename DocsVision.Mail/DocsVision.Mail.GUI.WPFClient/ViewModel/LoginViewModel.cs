using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Configuration;
using System.Windows.Controls;

namespace DocsVision.Mail.GUI.WPFClient.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private ServiceClient _client;

        public LoginViewModel()
        {
            _client = new ServiceClient(ConfigurationManager.AppSettings["hostdomain"]);
        }

        private String login;
        public String Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }   

        public ICommand LogInCommand
        {
            get => new BaseCommand((object obj) =>
            {
                // PasswordBox box = obj as PasswordBox;

                try
                {
                    // Guid id = _client.Login(Login, box.Password).Id;    
                    OnShowView(this, new MainViewModel(Guid.NewGuid()));

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });
        }

        public ICommand RegCommand
        {
            get => new BaseCommand((object ojb) =>
            {
                OnShowView(this, new MainViewModel(Guid.NewGuid()));
            });
        }
    }
}
