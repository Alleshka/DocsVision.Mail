using System;
using System.Configuration;
using System.Windows.Input;
using System.Collections.Generic;
using DocsVision.Mail.Model;
using System.Linq;
using System.Collections.ObjectModel;


namespace DocsVision.Mail.GUI.WPFClient.ViewModel
{
    public class SendLetterViewModel : BaseViewModel
    {
        private ServiceClient _client;
        private Guid curUserId;

        public SendLetterViewModel(Guid user)
        {
            curUserId = user;
            _client = new ServiceClient(ConfigurationManager.AppSettings["hostdomain"]);
        }

        private String head;
        public String Head
        {
            get => head;
            set
            {
                head = value;
                OnPropertyChanged("Head");
            }
        }

        private String contet;
        public String Content
        {
            get => contet;
            set
            {
                contet = value;
                OnPropertyChanged("Content");
            }
        }

        private ObservableCollection<Employee> employeeList;
        public ObservableCollection<Employee> EmployeeList
        {
            get => employeeList;
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private String login;
        public String ReceiverLogn
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged("ReceiverLogn");
            }
        }

        public ICommand SendMessage
        {
            get => new BaseCommand((object obj) =>
            {
                _client.SendLetter(new Letter()
                {
                    Head = head,
                    ContentMessage = contet,
                    Sender = _client.GetUserInfo(curUserId)
                }, employeeList.ToList());
                EmployeeList.Clear();
                Head = "";
                Content = "";
                ReceiverLogn = "";
            });
        }
        public ICommand AddReceiver
        {
            get => new BaseCommand((object obj) =>
            {
                Employee tmp = _client.FindUserByID(login);
                ReceiverLogn = "";
                if (employeeList == null) EmployeeList = new ObservableCollection<Employee>();
                employeeList.Add(tmp);
            });
        }
        
    }
}
