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
using System.Windows.Shapes;

namespace CourseWork1
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
            MenuFrame.Content = new DefaultPAge();
        }


        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ReturnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();

            var button = sender as Button;
            var window = Window.GetWindow(button);
            window.Close();
            objMainWindow.Show();
        }

        private void Instructions_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu_Click(sender, e);
            MenuFrame.Content = null;
            MenuFrame.Content = new Instructions();
        }

        private void Encoding_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu_Click(sender, e);
            MenuFrame.Content = null;
            MenuFrame.Content = new Encoding();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu_Click(sender, e);
            MenuFrame.Content = null;
            MenuFrame.Content = new TestPage();
        }

        private void MenuFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            
        }


        private void Account_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu_Click(sender, e);
            MenuFrame.Content = null;
            MenuFrame.Content = new AccountPage();
        }
    }
}
