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
using System.IO;

namespace CourseWork1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                File.WriteAllText(@"results.txt", "");
                File.WriteAllText(@"finaltest.txt", "");
            } 
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void OpenWindow(object sender, RoutedEventArgs e)
        {
            MenuWindow objMenuWindow = new MenuWindow();

            var button = sender as Button;
            var window = Window.GetWindow(button);
            window.Close();
            objMenuWindow.Show();
        }
    }
}
