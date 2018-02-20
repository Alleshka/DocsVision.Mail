using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Configuration;
using DocsVision.Mail.Model;
using System.Windows.Input;

namespace DocsVision.Mail.GUI.WPFClient.ViewModel
{
    public class LetterListViewModel : INotifyPropertyChanged
    {
        private Guid curUserId;
        private ServiceClient _client;

        public LetterListViewModel(Guid id)
        {
            Guid.TryParse("3e34bb2f-5692-4393-b19f-e4d7b5696cae", out curUserId);
            _client = new ServiceClient(ConfigurationManager.AppSettings["hostdomain"]);
        }

        private List<Letter> listLetter;
        public List<Letter> ListLetter
        {
            get => listLetter;
            set
            {
                listLetter = value;
                OnPropertyChanged("ListLetter");
            }
        }

        private Letter selectLetter;
        public Letter SelectLetter
        {
            get
            {
                return selectLetter;
            }
            set
            {
                selectLetter = value;
                ReadLetter.Execute(null);
                AutorName = _client.GetUserInfo(selectLetter.Sender).Login;
                OnPropertyChanged("SelectLetter");
            }
        }

        private String autorName;
        public String AutorName
        {
            get => autorName;
            set
            {
                autorName = value;
                OnPropertyChanged("AutorName");
            }
        }

        public ICommand LoadNewLetter
        {
            get => new BaseCommand((object obj) => 
                {
                    ListLetter = _client.GetNewLetters(curUserId);
                });
        }
        public ICommand LoadAllLetter
        {
            get => new BaseCommand((object obj) =>
            {
                ListLetter = _client.GetAllLetters(curUserId);
            });
        }
        public ICommand ReadLetter
        {
            get => new BaseCommand((object obj) =>
            {
                _client.ReadLetter(SelectLetter.Id, curUserId);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}