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
    /// Логика взаимодействия для Encoding.xaml
    /// </summary>
    public partial class Encoding : Page
    {
        public Encoding()
        {
            InitializeComponent();
        }

        private void SubmitButton1_Click(object sender, RoutedEventArgs e)
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

            string word = UserInputWord.Text;

            bool flag = true;
            if (word == String.Empty)
            {
                MessageBox.Show("Type something. Input is empty!");
                flag = false;
            } else if (encodings.Count == 0)
            {
                MessageBox.Show("Choose something!");
                flag = false;
            }

            foreach (char c in word)
            {
                int unicode = c;
                if (unicode >= 128)
                {
                    MessageBox.Show("Please, type only English letters!");
                    flag = false;
                    break;
                }
            }

            if (flag)
            {
                // Отправляем на новую страницу
                DrawEncoding newWindow = new DrawEncoding(word, encodings);

                var button = sender as Button;
                var window = Window.GetWindow(button);
                window.Close();
                newWindow.Show();
            }
        }
    }
}
