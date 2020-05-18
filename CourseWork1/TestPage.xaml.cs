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

namespace CourseWork1
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        public TestPage()
        {
            InitializeComponent();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            TestWindow newWindow = new TestWindow();

            var button = sender as Button;
            var window = Window.GetWindow(button);
            window.Close();
            newWindow.Show();
        }

        private void finalTest_Click(object sender, RoutedEventArgs e)
        {
            FinalTestWindow newWindow = new FinalTestWindow();

            var button = sender as Button;
            var window = Window.GetWindow(button);
            window.Close();
            newWindow.Show();
        }
    }
}
