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
using EncodingLibrary;

namespace CourseWork1
{
    /// <summary>
    /// Логика взаимодействия для EachEncodingPage.xaml
    /// </summary>
    public partial class EachEncodingPage : Page
    {
        public List<IEncoding> Encodings { get; set; }
        public int CurrentStep { get; set; }

        public EachEncodingPage()
        {
            InitializeComponent();
        }

        public EachEncodingPage(List<IEncoding> encodings, int currentStep)
        {
            InitializeComponent();
            Encodings = encodings;
            CurrentStep = currentStep;

            ExampleButton.Visibility = Visibility.Visible;

            // Определяем, какие кнопки показывать, а какие - нет
            if (Encodings.Count == 1)
            {
                NextButton.Visibility = Visibility.Hidden;
                BackButton.Visibility = Visibility.Hidden;
            }
            else
            {
                if (currentStep == 0)
                {
                    BackButton.Visibility = Visibility.Hidden;
                    NextButton.Visibility = Visibility.Visible;
                }
                else if (currentStep == Encodings.Count - 1)
                {
                    NextButton.Visibility = Visibility.Hidden;
                    BackButton.Visibility = Visibility.Visible;
                }
                else
                {
                    NextButton.Visibility = Visibility.Visible;
                    BackButton.Visibility = Visibility.Visible;
                }
            }

            EncodingName.Text = Encodings[CurrentStep].Name;
            EncodingExplain.Text = Encodings[CurrentStep].StepByStep;
        }

        private void ExampleButton_Click(object sender, RoutedEventArgs e)
        {
            ExampleWindow newWindow = new ExampleWindow(Encodings, CurrentStep);

            newWindow.Show();
            //MessageBox.Show(Encodings[CurrentStep].Example);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EachEncodingPage(Encodings, ++CurrentStep));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EachEncodingPage(Encodings, --CurrentStep));
        }
    }
}
