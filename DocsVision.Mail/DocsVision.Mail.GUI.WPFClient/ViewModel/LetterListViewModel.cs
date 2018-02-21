using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Configuration;
using DocsVision.Mail.Model;
using System.Windows.Input;

namespace DocsVision.Mail.GUI.WPFClient.ViewModel
{
    public class LetterListViewModel : BaseViewModel
    {
        private Guid curUserId;
        private ServiceClient _client;

        public LetterListViewModel(Guid id, ServiceClient client)
        {
            curUserId = id;
            _client = client;

            LoadNewLetter.Execute(null);
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
                OnPropertyChanged("SelectLetter");
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
                try
                {
                    ListLetter = _client.GetAllLetters(curUserId);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });
        }
        public ICommand ReadLetter
        {
            get => new BaseCommand((object obj) =>
            {
                if (selectLetter.IsRead == false)
                {
                    _client.ReadLetter(SelectLetter.Id, curUserId);
                    SelectLetter.IsRead = true;
                }
            });
        }
    }
}