using System;
using System.Collections.Generic;
using EncodingLibrary;
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
    public partial class FinalTestWindow : Window
    {
        public FinalTestWindow()
        {
            InitializeComponent();
            TestTasksFrame.Content = new FinalTestTasksPage(0);
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow newWindow = new MenuWindow();

            var button = sender as Button;
            var window = Window.GetWindow(button);
            window.Close();
            newWindow.Show();
        }


        private void NextTask_Click(object sender, RoutedEventArgs e)
        {
            //CurrentStep += 1;
            //string newCode = GetRandomString(rnd.Next(3, 7));
            //newCode = UniteBinaryValues(FromIntToByte(FromTextToASCII(Word)));
            //Polyline polyline = Encodings[CurrentStep].GetGraph(newCode);
            //canGraph.Children.Add(polyline);    
        }
    }
}
