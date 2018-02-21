using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DocsVision.Mail.GUI.WPFClient.View;

namespace DocsVision.Mail.GUI.WPFClient.ViewModel
{
    public class ContentPresenterViewModel : BaseViewModel
    {
        private BaseViewModel _view;
        private String _title;

        public String Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public BaseViewModel ViewModel
        {
            get => _view;
            set
            {
                _view = value;
                OnPropertyChanged("ViewModel");
            }
        }

        public ContentPresenterViewModel()
        {
            Title = "TestMailProgram";
            ShowView(this, new LoginViewModel());
        }

        public void ShowView(object sender, BaseViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModel.ShowViewEvent += ShowView;
        }
    }
}
