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
using System.IO;
using Path = System.Windows.Shapes.Path;

namespace CourseWork1
{
    /// <summary>
    /// Логика взаимодействия для SurtainTestWindow.xaml
    /// </summary>
    public partial class SurtainTestWindow : Window
    {
        public List<IEncoding> Encodings { get; set; }

        public static Random rnd = new Random();

        public string Word { get; set; }

        public static string GetRandomString(int length)
        {
            string res = String.Empty;
            for (int i = 0; i < length; i++)
                res += (char) rnd.Next('a', 'z' + 1);

            return res;
        }

        public SurtainTestWindow()
        {
            InitializeComponent();
            
        }

        public System.Windows.Shapes.Path GetXAxis()
        {
            // Рисование
            const double margin = 10;
            double xmin = margin;
            // double xmax = canGraph.Width - margin;
            // double ymin = margin;
            double ymax = canGraph.Height - margin;
            const double step = 20;

            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymax), new Point(canGraph.Width, ymax)));
            for (double x = xmin + step;
                x <= canGraph.Width - step; x += step)
            {
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, ymax - margin / 2),
                    new Point(x, ymax + margin / 2)));
            }

            System.Windows.Shapes.Path xaxis_path = new System.Windows.Shapes.Path
            {
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Data = xaxis_geom
            };

            // canGraph.Children.Add(xaxis_path);
            return xaxis_path;
        }

        public SurtainTestWindow(List<IEncoding> encodings)
        {
            InitializeComponent();
            CorrectIcon.Visibility = Visibility.Hidden;
            WrongIcon.Visibility = Visibility.Hidden;

            Encodings = encodings;

            // Генерируем случайное слово 
            Word = GetRandomString(rnd.Next(3, 6 + 1));

            MessageBox.Show(Word);

            // Получение большой строки значений
            string code = UniteBinaryValues(FromIntToByte(FromTextToASCII(Word)));

            // Рисование
            const double margin = 10;
            double xmin = margin;
            // double xmax = canGraph.Width - margin;
            // double ymin = margin;
            double ymax = canGraph.Height - margin;
            const double step = 20;

            canGraph.Children.Add(GetXAxis());

            // Make the Y ayis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, 0), new Point(xmin, canGraph.Height)));
            for (double y = step; y <= canGraph.Height - step; y += step)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin - margin / 2, y),
                    new Point(xmin + margin / 2, y)));
            }

            Path yaxis_path = new Path
            {
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Data = yaxis_geom
            };

            canGraph.Children.Add(yaxis_path);

            Polyline polyline = Encodings[0].GetGraph(code);
            canGraph.Children.Add(polyline);

            canGraph.Children.Add(Encodings[0].GetSyncTact(code));

            canGraph.Width = (polyline.Points[polyline.Points.Count - 1].X + 50);
            canGraph.Children.Add(GetXAxis());
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            TestWindow newWindow = new TestWindow();

            var button = sender as Button;
            var window = Window.GetWindow(button);
            window.Close();
            newWindow.Show();
        }


        /// <summary>
        /// метод, который переводит текст в ascii код
        /// </summary>
        /// <param name="text"> изначальное слово </param>
        /// <returns> список ascii кодов по буквам </returns>
        public static List<int> FromTextToASCII(string text)
        {
            List<int> codes = new List<int>();
            foreach (char c in text)
            {
                int unicode = c;
                if (unicode < 128)
                    codes.Add(unicode);
                else
                    // Нужно придумать более умную реализацию, не работает для кирилицы
                    throw new ArgumentException("Non ascii symbol");
            }

            return codes;
        }

        /// <summary>
        /// метод, который переводит числа в двоичную систему и возвращает 8-битовую строку
        /// </summary>
        /// <param name="codes"> список значений ASCII </param>
        /// <returns> список переведенных строк </returns>
        public static List<string> FromIntToByte(List<int> codes)
        {
            List<string> bytes = new List<string>();
            for (int i = 0; i < codes.Count; i++)
            {
                string binary = (Convert.ToString(codes[i], 2));

                // Добавляем ведущие нули, чтобы всегда было 8 бит
                bytes.Add(binary.PadLeft(8, '0'));
            }

            return bytes;
        }

        /// <summary>
        /// метод, который из списка составляет большую строку для рисования
        /// </summary>
        /// <param name="bytes"> список двочиных значений по буквам </param>
        /// <returns> единая строка значений </returns>
        public static string UniteBinaryValues(List<string> bytes)
        {
            string res = String.Empty;
            for (int i = 0; i < bytes.Count; i++)
                res += bytes[i];

            return res;
        }

        string answer;

        private void submitAnswer_Click(object sender, RoutedEventArgs e)
        {
            answer = userAnswer.Text;
            if (answer == Word)
            {
                CorrectIcon.Visibility = Visibility.Visible;
                WrongIcon.Visibility = Visibility.Hidden;

                submitAnswer.Visibility = Visibility.Hidden;

                try
                {
                    File.AppendAllText(@"results.txt", Encodings[0].Name + " ");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }    
            else
            {
                WrongIcon.Visibility = Visibility.Visible;
                CorrectIcon.Visibility = Visibility.Hidden;
            }
                
        }

        private void NextTry_Click(object sender, RoutedEventArgs e)
        {
            SurtainTestWindow newWindow = new SurtainTestWindow(Encodings);

            var button = sender as Button;
            var window = Window.GetWindow(button);
            window.Close();
            newWindow.Show();
        }
    }
}
