using System;
using EncodingLibrary;
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
    /// Логика взаимодействия для Instructions.xaml
    /// </summary>
    public partial class Instructions : Page
    {
        public Instructions()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            List<IEncoding> encodings = new List<IEncoding>();

            if ((bool)ALLinst.IsChecked)
            {
                RZinst.IsChecked = true; 
                NRZinst.IsChecked = true;
                NRZIinst.IsChecked = true;
                MANCHinst.IsChecked = true;
                DMANCHinst.IsChecked = true;
                MLT3inst.IsChecked = true;
                TwoB1Qinst.IsChecked = true;
            }

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

            if (encodings.Count > 0)
            {
                InstructionsWindow instWindow = new InstructionsWindow(encodings, 0);
                var button = sender as Button;
                var window = Window.GetWindow(button);
                window.Close();
                instWindow.Show();
            }
            else
                MessageBox.Show("Choose something!");
        }

        private void ALLinst_Checked(object sender, RoutedEventArgs e)
        {
            RZinst.IsChecked = true;
            NRZinst.IsChecked = true;
            NRZIinst.IsChecked = true;
            MANCHinst.IsChecked = true;
            DMANCHinst.IsChecked = true;
            MLT3inst.IsChecked = true;
            TwoB1Qinst.IsChecked = true;
        }

        private void ALLinst_Unchecked(object sender, RoutedEventArgs e)
        {
            RZinst.IsChecked = false;
            NRZinst.IsChecked = false;
            NRZIinst.IsChecked = false;
            MANCHinst.IsChecked = false;
            DMANCHinst.IsChecked = false;
            MLT3inst.IsChecked = false;
            TwoB1Qinst.IsChecked = false;
        }
    }
}
