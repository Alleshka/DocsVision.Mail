using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Input;

namespace DocsVision.Mail.GUI.WPFClient.ViewModel
{
    public class RegViewModel : BaseViewModel
    {
        private ServiceClient _client;

        public RegViewModel()
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

        private String firstName;
        public String FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private String lastName;
        public String LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public ICommand CreateCommand
        {
            get => new BaseCommand((object obj) =>
            {
                System.Windows.Controls.PasswordBox box = obj as System.Windows.Controls.PasswordBox;

                try
                {
                    var id = _client.CreateEmployee(new Model.Employee()
                    {
                        Login = login,
                        Password = box.Password,
                        FirstName = firstName,
                        LastName = lastName
                    }).Id;
                    OnShowView(this, new MainViewModel(id));
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });
        }

        public ICommand BackCommand
        {
            get => new BaseCommand((object obj) =>
            {
                OnShowView(this, new LoginViewModel());
            });
        }
    }
}
