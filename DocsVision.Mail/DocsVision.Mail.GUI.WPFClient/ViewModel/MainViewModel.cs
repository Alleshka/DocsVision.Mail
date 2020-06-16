using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DocsVision.Mail.Model;
using System.Windows.Input;

namespace DocsVision.Mail.GUI.WPFClient.ViewModel
{
    public enum ViewMode
    {
        LettersList,
        SendLetter
    }

    public class MainViewModel : BaseViewModel
    {
        private ServiceClient _client;
        private Guid curUserId;

        private SendLetterViewModel sendLetter;
        private LetterListViewModel letterList;

        public MainViewModel(Guid user)
        {
            curUserId = user;
            _client = new ServiceClient(ConfigurationManager.AppSettings["hostdomain"]);

            SendLetter = new SendLetterViewModel(curUserId, _client);
            letterList = new LetterListViewModel(curUserId, _client);

            IsCheckBox = false;
        }

        public SendLetterViewModel SendLetter
        {
            get => sendLetter;
            set
            {
                sendLetter = value;
                OnPropertyChanged("SendLetter");
            }
        }

        public LetterListViewModel LetterList
        {
            get => letterList;
            set
            {
                letterList = value;
                OnPropertyChanged("LetterList");
            }
        }

        public ICommand LogOut
        {
            get => new BaseCommand((object obj) =>
            {
                OnShowView(this, new LoginViewModel());
            });
        }

        private bool _isCheckBox;
        public bool IsCheckBox
        {
            get => _isCheckBox; 
            set
            {
                _isCheckBox = value;
                CurViewModel = (_isCheckBox ? SendLetter as BaseViewModel : LetterList as BaseViewModel);

                OnPropertyChanged("IsCheckBox");
            }
        }

        private BaseViewModel _curViewModel;
        public BaseViewModel CurViewModel
        {
            get => _curViewModel;
            set
            {
                _curViewModel = value;
                OnPropertyChanged("CurViewModel");
            }
        }
    }
}
