using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EncodingLibrary;

namespace CourseWork1
{
    /// <summary>
    /// Логика взаимодействия для InstructionsWindow.xaml
    /// </summary>
    public partial class InstructionsWindow : Window
    {
        public List<IEncoding> Encodings { get; set; }
        public int CurrentStep { get; set; }

        public InstructionsWindow()
        {
            
        }

        public InstructionsWindow(List<IEncoding> encodings, int currentStep)
        {
            InitializeComponent();
            Encodings = encodings;
            CurrentStep = currentStep;

            EachEncodingFrame.Navigate(new EachEncodingPage(Encodings, CurrentStep));
        }

        private void StartEnc_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow newWindow = new MenuWindow();

            var button = sender as Button;
            var window = Window.GetWindow(button);
            window.Close();
            newWindow.Show();
        }
    }
}
