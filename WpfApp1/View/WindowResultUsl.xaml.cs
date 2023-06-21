using System.Windows;
using WpfApp1.Model;
using WpfApp1.Model.Date;
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Логика взаимодействия для WindowResultUsl.xaml
    /// </summary>
    public partial class WindowResultUsl : Window
    {
        public WindowResultUsl()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
