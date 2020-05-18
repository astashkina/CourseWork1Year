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
    /// <summary>
    /// Логика взаимодействия для ExampleWindow.xaml
    /// </summary>
    public partial class ExampleWindow : Window
    {
        public List<IEncoding> Encodings { get; set; }
        public int CurrentStep { get; set; }

        public ExampleWindow()
        {
            InitializeComponent();
        }

        public ExampleWindow(List<IEncoding> encodings, int currentStep)
        {
            InitializeComponent();
            Encodings = encodings;
            CurrentStep = currentStep;

            Examples.Text = Encodings[CurrentStep].Example;
        }

        private void BackToInstructions_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
