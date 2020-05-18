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
using EncodingLibrary;

namespace CourseWork1
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            List<IEncoding> encodings = new List<IEncoding>();

            if ((bool)RZinst.IsChecked)
            {
                RZ rz = new RZ();
                encodings.Add(rz);
            }
            if ((bool)NRZinst.IsChecked)
            {
                NRZ nrz = new NRZ();
                encodings.Add(nrz);
            }
            if ((bool)NRZIinst.IsChecked)
            {
                NRZI nrzi = new NRZI();
                encodings.Add(nrzi);
            }
            if ((bool)MANCHinst.IsChecked)
            {
                MANCH manch = new MANCH();
                encodings.Add(manch);
            }
            if ((bool)DMANCHinst.IsChecked)
            {
                DMANCH dmanch = new DMANCH();
                encodings.Add(dmanch);
            }
            if ((bool)MLT3inst.IsChecked)
            {
                MLT3 mlt3 = new MLT3();
                encodings.Add(mlt3);
            }
            if ((bool)TwoB1Qinst.IsChecked)
            {
                TwoB1Q twob1q = new TwoB1Q();
                encodings.Add(twob1q);
            }

            if (encodings.Count != 0)
            {
                SurtainTestWindow newWindow = new SurtainTestWindow(encodings);

                var button = sender as Button;
                var window = Window.GetWindow(button);
                window.Close();
                newWindow.Show();
            }
            else
            {
                MessageBox.Show("You should choose something");
            }
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
