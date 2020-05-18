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
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public AccountPage()
        {
            InitializeComponent();

            int countRZ = 0, countNRZ = 0, countNRZI = 0, countMANCH = 0, 
                countMANCHD = 0, countMLT3 = 0, count2B1Q = 0;

            int result = 0;

            string results = String.Empty;
            string finalResults = String.Empty;
            try
            {
                results = File.ReadAllText(@"results.txt");
                finalResults = File.ReadAllText(@"finaltest.txt");
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            string[] encodings = results.Split(' ');
            foreach (var t in encodings)
            {
                if (t == "RZ") countRZ++;
                if (t == "NRZ") countNRZ++;
                if (t == "NRZI") countNRZI++;
                if (t == "MANCH") countMANCH++;
                if (t == "DMANCH") countMANCHD++;
                if (t == "MLT3") countMLT3++;
                if (t == "2B1Q") count2B1Q++;
            }

            TextRZ.Text += countRZ;
            TextNRZ.Text += countNRZ;
            TextNRZI.Text += countNRZI;
            TextMANCH.Text += countMANCH;
            TextDMANCH.Text += countMANCHD;
            TextMLT3.Text += countMLT3;
            Text2B1Q.Text += count2B1Q;

            string[] finalResultEncodings = finalResults.Split(' ');
            foreach (var t in finalResultEncodings)
                if (t != " " && t != "") result++;

            TextResult.Text += result;
        }
    }
}
