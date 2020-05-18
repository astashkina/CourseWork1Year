using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для DrawEncoding.xaml
    /// </summary>
    public partial class DrawEncoding : Window
    {
        public List<IEncoding> Encodings { get; set; }
        public string Word { get; set; }

        public DrawEncoding()
        {
            InitializeComponent();
        }

        public DrawEncoding(string word, List<IEncoding> encodings)
        {
            InitializeComponent();
            Encodings = encodings;
            Word = word;

            SaveImage.Visibility = Visibility.Hidden;
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

        private void StartEnc_Click(object sender, RoutedEventArgs e)
        {
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

            System.Windows.Shapes.Path yaxis_path = new System.Windows.Shapes.Path
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

            StartEnc.Visibility = Visibility.Hidden;
            SaveImage.Visibility = Visibility.Visible;
        }


        public void ExportToPng(string path, Canvas surface)
        {
            if (path == null) return;

            // Save current canvas transform
            Transform transform = surface.LayoutTransform;
            // reset current transform (in case it is scaled or rotated)
            surface.LayoutTransform = null;

            // Get the size of canvas
            Size size = new Size(surface.Width, surface.Height);
            // Measure and arrange the surface
            // VERY IMPORTANT
            surface.Measure(size);
            surface.Arrange(new Rect(size));

            // Create a render bitmap and push the surface to it
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(surface);

            // Create a file stream for saving image
            using (FileStream outStream = new FileStream(path, FileMode.Create))
            {
                // Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
            }
        }

        private void SaveImage_Click(object sender, RoutedEventArgs e)
        {
            ExportToPng(Encodings[0].Name + ".png", canGraph);
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
