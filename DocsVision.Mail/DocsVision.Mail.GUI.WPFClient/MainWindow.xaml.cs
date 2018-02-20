using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DocsVision.Mail.GUI.WPFClient.ViewModel;

namespace DocsVision.Mail.GUI.WPFClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Guid.TryParse("3E34BB2F-5692-4393-B19F-E4D7B5696CAE", out Guid id);
            InBoxTab.DataContext = new LetterListViewModel(id);
            SendBoxTab.DataContext = new SendLetterViewModel(id);
        }
    }
}
