using System;
using System.Collections.Generic;
using System.Drawing;
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


namespace EncodingLibrary
{
    /// <summary>
    ///     Интерфейс IEncoding. Три свойства, которые переопределяется в каждом классе кодировки
    /// </summary>
    public interface IEncoding
    {
        string Name { get; set; }
        string StepByStep { get; set; }
        string Example { get; set; }
        Polyline GetGraph(string code);
        Polyline GetSyncTact(string code);
    }

    /// <summary>
    ///     Абстрактный класс кодировки, реализаует интерфейс и все его свойства (просто get; set;)
    /// </summary>
    public abstract class Encoding : IEncoding
    {
        protected Encoding() { }

        public string Name { get; set; }

        public string Example { get; set;}

        public string StepByStep{ get; set; }

        public abstract Polyline GetGraph(string code);

        public abstract Polyline GetSyncTact(string code);
    }

    /// <summary>
    ///     Класс кодировки RZ
    /// </summary>
    public class RZ : Encoding
    {
        /// <summary>
        ///     Конструктор класса RZ. Он переопределяет все свойства. Пока не сделано для каждого класса, но теперь 
        ///     видны эти свойства отовсюду
        /// </summary>
        public RZ() 
        {
            Name = "RZ";
            StepByStep = "\n\nRZ step-by-step algorithm: \n" +
                        "To transmit 1 and 0, three constantly distinguishable potentials are used: \n" +
                        "\t -> bits 0 are transmitted by high level \n" +
                        "\t -> bits 1 are transmitted by low level \n" +
                        "\t -> always returns to zero level \n" +
                        "\t -> read by front edge \n\n" +
                        "Firstly, we need to use ASCII table to see the code of each letter in our message.\n" +
                        "Secondly, we have to transform this letter codes into binary numeral system.\n" +
                        "The next step is the implementation of four potentials described above.\n" +
                        "The encoding is ready!\n\n";
            Example =  "\n\nLet's encrypt the word 'IT' as an example.\n" +
                        "Leaning on ASCII table we find codes of both letters: I = 73; T = 84.\n" +
                        "Now we transform this codes into binary numeral system: 73 = 01001001; 84 = 01010100.\n" +
                        "Now we are just following the RZ rules:\n" +
                        "\tWe start from the bottom middle level and read by front edge. The first number is 0.\n" +
                        "\t\t => we code it as just _ (middle and high levels). \n" +
                        "\t The next number is 1 => we code it as |_|‾  (low and high levels). \n" +
                        "Good job! Continue doing the same job and you will reach the encoding! You can check yourself " +
                        "by encoding word IT (2 button in menu).\n\n";
        }

        public override Polyline GetGraph(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            if (code[0] == '1')
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(50, 60);
                System.Windows.Point point3 = new System.Windows.Point(60, 60);
                System.Windows.Point point4 = new System.Windows.Point(60, 80);
                System.Windows.Point point5 = new System.Windows.Point(70, 80);
                points.Add(point1); points.Add(point2); points.Add(point3);  points.Add(point4); points.Add(point5);
            }
            else
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(70, 80);
                points.Add(point1); points.Add(point2);
            }

            for (int i = 1; i < code.Length; i++)
            {
                if (code[i] == '0')
                {
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 20, points[points.Count - 1].Y));
                }
                else if (code[i] == '1')
                {
                    points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                    points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                }
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Green;
            polyline.Points = points;

            return polyline;
        }

        public override Polyline GetSyncTact(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            System.Windows.Point point1 = new System.Windows.Point(50, 40);
            System.Windows.Point point2 = new System.Windows.Point(50, 20);
            System.Windows.Point point3 = new System.Windows.Point(60, 20);
            System.Windows.Point point4 = new System.Windows.Point(60, 40);
            System.Windows.Point point5 = new System.Windows.Point(70, 40);

            points.Add(point1); points.Add(point2); points.Add(point3); points.Add(point4); points.Add(point5);
            
            for (int i = 1; i < code.Length; i++)
            {
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Aqua;
            polyline.Points = points;

            return polyline;
        }
    }

    /// <summary>
    ///     Дальше все сделано, но комментов нет
    /// </summary>
    public class NRZ : Encoding
    {
        public NRZ()
        {
            Name = "NRZ";
            StepByStep = "\n\nNRZ step-by-step algorithm: \n" +
                        "To transmit 1 and 0, three constantly distinguishable potentials are used: \n" +
                        "\t -> bits 0 are transmitted by low level \n" +
                        "\t -> bits 1 are transmitted by high level \n" +
                        "\t -> read by front edge \n\n" +
                        "Firstly, we need to use ASCII table to see the code of each letter in our message.\n" +
                        "Secondly, we have to transform this letter codes into binary numeral system.\n" +
                        "The next step is the implementation of three potentials described above.\n" +
                        "The encoding is ready!\n\n";
            Example =   "\n\nLet's encrypt the word 'IT' as an example.\n" +
                        "Leaning on ASCII table we find codes of both letters: I = 73; T = 84.\n" +
                        "Now we transform this codes into binary numeral system: 73 = 01001001; 84 = 01010100.\n" +
                        "Now we are just following the NRZ rules:\n" +
                        "\tWe start from the bottom left corner and read by front edge. The first number is 0.\n" +
                        "\t\t => we code it as just _ (because 0 is transmitted by low level). \n" +
                        "\t The next number is 1 => we code it as |‾ (because 1 are transmitted by high level). \n" +
                        "\t The next number is also 0 => we code it as |_ .\n" +
                        "\t The next number is also 0 => we code it as _ (this bit is similar with the previous one so we do not change the level).\n\n" +
                        "Good job! Continue doing the same job and you will reach the encoding! You can check yourself by encoding word IT (2 key in menu).\n\n";
        }

        /// <summary>
        /// метод, который возвращает граф NRZ
        /// </summary>
        /// <param name="code"> длинная строка значений </param>
        /// <returns> Polyline объект с графом NRZ </returns>
        public override Polyline GetGraph(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            if (code[0] == '1')
            {
                // Point point1 = new Point(0, 0);
                // Point point2 = new Point(0, -10);
                // Point point3 = new Point(10, -10);
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(50, 60);
                System.Windows.Point point3 = new System.Windows.Point(60, 60);
                points.Add(point1); points.Add(point2); points.Add(point3);
            }
            else
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(60, 80);
                points.Add(point1); points.Add(point2);
            }

            for (int i = 1; i < code.Length; i++)
            {
                if (code[i] == '0')
                {
                    if (code[i - 1] == '0')
                    {
                        points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                    }
                    else
                    {
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                        points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                    }
                }
                else if (code[i] == '1')
                {
                    if (code[i - 1] == '1')
                    {
                        points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                    }
                    else
                    {
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                        points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                    }
                }
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Green;
            polyline.Points = points;

            return polyline;
        }

        public override Polyline GetSyncTact(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            System.Windows.Point point1 = new System.Windows.Point(50, 40);
            System.Windows.Point point2 = new System.Windows.Point(50, 20);
            System.Windows.Point point3 = new System.Windows.Point(60, 20);
            System.Windows.Point point4 = new System.Windows.Point(60, 40);
            System.Windows.Point point5 = new System.Windows.Point(70, 40);

            points.Add(point1); points.Add(point2); points.Add(point3); points.Add(point4); points.Add(point5);

            for (int i = 1; i < code.Length / 2; i++)
            {
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Aqua;
            polyline.Points = points;

            return polyline;
        }

    }

    public class NRZI : Encoding
    {
        public NRZI()
        {
            Name = "NRZI";
            StepByStep ="\n\nNRZI step-by-step algorithm: \n" +
                        "To transmit 1 and 0, three constantly distinguishable potentials are used: \n" +
                        "\t -> bits 0 do not change level\n" +
                        "\t -> bits 1 change level on the opposite \n" +
                        "\t -> read by front edge \n\n" +
                        "Firstly, we need to use ASCII table to see the code of each letter in our message.\n" +
                        "Secondly, we have to transform this letter codes into binary numeral system.\n" +
                        "The next step is the implementation of three potentials described above.\n" +
                        "The encoding is ready!\n\n";
            Example =   "\n\nLet's encrypt the word 'IT' as an example.\n" +
                        "Leaning on ASCII table we find codes of both letters: I = 73; T = 84.\n" +
                        "Now we transform this codes into binary numeral system: 73 = 01001001; 84 = 01010100.\n" +
                        "Now we are just following the NRZI rules:\n" +
                        "\tWe start from the bottom left corner and read by front edge. The first number is 0.\n" +
                        "\t\t => we code it as just _ (because 0 is transmitted by low level). \n" +
                        "\t The next number is 1 => we code it as |‾ (because 1 are transmitted by high level). \n" +
                        "\t The next number is also 0 => we stay at high level ‾.\n" +
                        "Good job! Continue doing the same job and you will reach the encoding! You can check " +
                        "yourself by encoding word IT (2 key in menu).\n\n";
        }

        /// <summary>
        /// метод, который возвращает граф NRZI
        /// </summary>
        /// <param name="code"> длинная строка значений </param>
        /// <returns> Polyline объект с графом NRZI </returns>
        public override Polyline GetGraph(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            if (code[0] == '1')
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(50, 60);
                System.Windows.Point point3 = new System.Windows.Point(60, 60);
                points.Add(point1); points.Add(point2); points.Add(point3);
            }
            else
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(60, 80);
                points.Add(point1); points.Add(point2);
            }

            for (int i = 1; i < code.Length; i++)
            {
                if (code[i] == '1')
                {
                    if (points[points.Count - 1].Y == 80)
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));

                    else
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));

                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                }
                else if (code[i] == '0')
                {
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                }
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Green;
            polyline.Points = points;

            return polyline;
        }

        public override Polyline GetSyncTact(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            System.Windows.Point point1 = new System.Windows.Point(50, 40);
            System.Windows.Point point2 = new System.Windows.Point(50, 20);
            System.Windows.Point point3 = new System.Windows.Point(60, 20);
            System.Windows.Point point4 = new System.Windows.Point(60, 40);
            System.Windows.Point point5 = new System.Windows.Point(70, 40);

            points.Add(point1); points.Add(point2); points.Add(point3); points.Add(point4); points.Add(point5);

            for (int i = 1; i < code.Length / 2; i++)
            {
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Aqua;
            polyline.Points = points;

            return polyline;
        }

    }

    public class MANCH : Encoding
    {
        public MANCH()
        {
            Name = "MANCH";
            StepByStep ="\n\nManchester step-by-step algorithm: \n" +
                        "To transmit 1 and 0, three constantly distinguishable potentials are used: \n" +
                        "\t -> bits 0 are transmitted from low level to high level\n" +
                        "\t -> bits 1 are transmitted from high level to low level\n" +
                        "Firstly, we need to use ASCII table to see the code of each letter in our message.\n" +
                        "Secondly, we have to transform this letter codes into binary numeral system.\n" +
                        "The next step is the implementation of two potentials described above.\n" +
                        "The encoding is ready!\n\n";
            Example =   "\n\nLet's encrypt the word 'IT' as an example.\n" +
                        "Leaning on ASCII table we find codes of both letters: I = 73; T = 84.\n" +
                        "Now we transform this codes into binary numeral system: 73 = 01001001; 84 = 01010100.\n" +
                        "Now we are just following the Manchester rules:\n" +
                        "\tWe start from the bottom left corner and read by front edge. The first number is 0.\n" +
                        "\t\t => we code it as just _|‾ . \n" +
                        "\t The next number is 1 => we code it as ‾|_ . \n" +
                        "\t If we have two 1 in a row it would be coded as  ‾|_|‾|_ .\n" +
                        "Good job! Continue doing the same job and you will reach the encoding! You " +
                        "can check yourself by encoding word IT (2 key in menu).\n\n";
        }

        /// <summary>
        /// метод, который возвращает граф MANCH
        /// </summary>
        /// <param name="code"> длинная строка значений </param>
        /// <returns> Polyline объект с графом MANCH </returns>
        public override Polyline GetGraph(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            if (code[0] == '1')
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(50, 60);
                System.Windows.Point point3 = new System.Windows.Point(60, 60);
                System.Windows.Point point4 = new System.Windows.Point(60, 80);
                System.Windows.Point point5 = new System.Windows.Point(70, 80);
                points.Add(point1); points.Add(point2); points.Add(point3); points.Add(point4); points.Add(point5);
            }
            else
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(60, 80);
                System.Windows.Point point3 = new System.Windows.Point(60, 60);
                System.Windows.Point point4 = new System.Windows.Point(70, 60);
                points.Add(point1); points.Add(point2); points.Add(point3); points.Add(point4);
            }

            for (int i = 1; i < code.Length; i++)
            {
                if (code[i] == '1')
                {
                    if (points[points.Count - 1].Y == 80)
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));

                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                    points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                }
                else if (code[i] == '0')
                {
                    if (points[points.Count - 1].Y == 60)
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));

                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                    points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                }
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Green;
            polyline.Points = points;

            return polyline;
        }

        public override Polyline GetSyncTact(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            System.Windows.Point point1 = new System.Windows.Point(50, 40);
            System.Windows.Point point2 = new System.Windows.Point(50, 20);
            System.Windows.Point point3 = new System.Windows.Point(60, 20);
            System.Windows.Point point4 = new System.Windows.Point(60, 40);
            System.Windows.Point point5 = new System.Windows.Point(70, 40);

            points.Add(point1); points.Add(point2); points.Add(point3); points.Add(point4); points.Add(point5);

            for (int i = 1; i < code.Length; i++)
            {
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Aqua;
            polyline.Points = points;

            return polyline;
        }
    }

    public class DMANCH : Encoding
    {
        public DMANCH()
        {
            Name = "DMANCH";
            StepByStep = "\n\nDMANCH step-by-step algoritm: \n" +
                         "\t->In the middle of each time period, a mandatory sync drop. \n " +
                         "\t->1 does not add a drop. \n" +
                         "\t->0 adds one more drop. \n" +
                         "\t->Beenig read by back edge \n\n" +
                         "First of all, we need to use ASCII table to see the code of each letter in our message.\n" +
                         "Secondly, we have to translate this letter codes into binary numeral system.\n" +
                         "The next step is the usage of potentials described above.\n" +
                         "The encoding is ready!\n\n";
            Example = "\n\nDMANCH step-by-step algoritm: \n" +
                        "\t->In the middle of each time period, a mandatory sync drop. \n " +
                        "\t->1 does not add a drop. \n" +
                        "\t->0 adds one more drop. \n" +
                        "\t->Beenig read by back edge \n\n" +
                        "First of all, we need to use ASCII table to see the code of each letter in our message.\n" +
                        "Secondly, we have to translate this letter codes into binary numeral system.\n" +
                        "The next step is the usage of potentials described above.\n" +
                        "The encoding is ready!\n\n";
        }

        /// <summary>
        /// метод, который возвращает граф DMANCH
        /// </summary>
        /// <param name="code"> длинная строка значений </param>
        /// <returns> Polyline объект с графом DMANCH </returns>
        public override Polyline GetGraph(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            if (code[0] == '0')
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(50, 60);
                System.Windows.Point point3 = new System.Windows.Point(60, 60);
                System.Windows.Point point4 = new System.Windows.Point(60, 80);
                System.Windows.Point point5 = new System.Windows.Point(70, 80);
                points.Add(point1); points.Add(point2); points.Add(point3); points.Add(point4); points.Add(point5);
            }
            else
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(60, 80);
                System.Windows.Point point3 = new System.Windows.Point(60, 60);
                System.Windows.Point point4 = new System.Windows.Point(70, 60);
                points.Add(point1); points.Add(point2); points.Add(point3); points.Add(point4);
            }

            for (int i = 1; i < code.Length; i++)
            {
                if (code[i] == '1')
                {
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));

                    if (points[points.Count - 1].Y == 80)
                    {
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                    }
                    else
                    {
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                    }
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                }
                else if (code[i] == '0')
                {
                    if (points[points.Count - 1].Y == 80)
                    {
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                        points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                        points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                    }
                    else
                    {
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                        points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y- 20));
                        points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                    }   


                }
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Green;
            polyline.Points = points;

            return polyline;
        }

        public override Polyline GetSyncTact(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            System.Windows.Point point1 = new System.Windows.Point(50, 40);
            System.Windows.Point point2 = new System.Windows.Point(50, 20);
            System.Windows.Point point3 = new System.Windows.Point(60, 20);
            System.Windows.Point point4 = new System.Windows.Point(60, 40);
            System.Windows.Point point5 = new System.Windows.Point(70, 40);

            points.Add(point1); points.Add(point2); points.Add(point3); points.Add(point4); points.Add(point5);

            for (int i = 1; i < code.Length; i++)
            {
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Aqua;
            polyline.Points = points;

            return polyline;
        }

    }

    public class MLT3 : Encoding
    {
        public MLT3() 
        {
            Name = "MLT3";
            StepByStep ="\n\nMLT-3 step-by-step algorithm: \n" +
                        "\t->The method is based on the cyclic switching of the levels -U, 0, + U. \n " +
                        "\t->A unit corresponds to a transition from one signal level to the next." +
                        "\t->As in the NRZI method, the signal does not change when transmitting 0." +
                        "\t->Beenig read by front edge. \n\n" +
                        "First of all, we need to use ASCII table to see the code of each letter in our message.\n" +
                        "Secondly, we have to translate this letter codes into binary numeral system.\n" +
                        "The next step is the usage of potentials described above.\n" +
                        "The encoding is ready!\n\n";
            Example =   "\n\nLet's encrypt the word 'IT' as an example.\n" +
                        "Leaning on ASCII table we find codes of both letters: I = 73; T = 84.\n" +
                        "Now we translate this codes into binary numeral system: 73 = 01001001; 84 = 01010100.\n" +
                        "Now we are just following the MLT-3 rules:\n" +
                        "\tWe start from the bottom left corner and read by front edge. The first number is 0.\n" +
                        "\t\t => we code it as just _ (because signal does not change when transmitting 0). \n" +
                        "\t The next number is 1 => we code it as |‾ (-U, 0)(because 1 are transferred by high level). \n" +
                        "\t The next number is 0 => we code it as _ (0).\n" +
                        "\t The next number is also 0 => we code it again as _ (0).\n" +
                        "\t The next number is 1 => we code it as |‾ (0, +U)\n " +
                        "\t The next number is 0 => we code it as - (+U)\n" +
                        "\t The next number is also 0 => we code it as - (+U)\n" +
                        "\t The next number is already 1 => we code it as |_ (+U, 0)\n" +
                        "Good job! Continue doing the same job and you will reach the encoding! You can check yourself " +
                        "by encoding word IT (2 key in menu).\n\n";
        }

        /// <summary>
        /// метод, который возвращает граф MLT3
        /// </summary>
        /// <param name="code"> длинная строка значений </param>
        /// <returns> Polyline объект с графом MLT3 </returns>
        public override Polyline GetGraph(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();
            int count = 0;

            if (code[0] == '0')
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(70, 80);
                points.Add(point1); points.Add(point2); 
            }
            else
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 100);
                System.Windows.Point point2 = new System.Windows.Point(50, 80);
                System.Windows.Point point3 = new System.Windows.Point(70, 80);
                points.Add(point1); points.Add(point2); points.Add(point3);
                count++;
            }

            for (int i = 1; i < code.Length; i++)
            {
                if (code[i] == '1')
                {
                    if (count % 4 == 1 || count % 4 == 2)
                    {
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                        points.Add(new System.Windows.Point(points[points.Count - 1].X + 20, points[points.Count - 1].Y));
                    }
                    else if (count % 4 == 3 || count % 4 == 0)
                    {
                        points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                        points.Add(new System.Windows.Point(points[points.Count - 1].X +20, points[points.Count - 1].Y));
                    }
                    count++;
                }
                else if (code[i] == '0')
                {
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 20, points[points.Count - 1].Y));
                }

            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Green;
            polyline.Points = points;

            return polyline;
        }

        public override Polyline GetSyncTact(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            System.Windows.Point point1 = new System.Windows.Point(50, 40);
            System.Windows.Point point2 = new System.Windows.Point(50, 20);
            System.Windows.Point point3 = new System.Windows.Point(60, 20);
            System.Windows.Point point4 = new System.Windows.Point(60, 40);
            System.Windows.Point point5 = new System.Windows.Point(70, 40);

            points.Add(point1); points.Add(point2); points.Add(point3); points.Add(point4); points.Add(point5);

            for (int i = 1; i < code.Length; i++)
            {
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Aqua;
            polyline.Points = points;

            return polyline;
        }
    }

    public class TwoB1Q : Encoding
    {
        public TwoB1Q()
        {
            Name = "2B1Q";
            StepByStep = "\n\n2B1Q step-by-step algorithm: \n" +
                        "\t->Transmits a couple of bits in one bit interval. \n " +
                        "\t->Allows you to transfer data twice as fast. \n" +
                        "\t->Beenig read by front edge. \n\n" +
                        "\t->It has 4 levels: 1st(from bottom) for 00, 2nd for 01, 3rd for 11 and 4th for 10. \n\n" +
                        "First of all, we need to use ASCII table to see the code of each letter in our message.\n" +
                        "Secondly, we have to translate this letter codes into binary numeral system.\n" +
                        "The next step is the usage of potentials described above.\n" +
                        "The encoding is ready!\n\n";
            Example = "\n\nLet's encrypt the word 'IT' as an example.\n" +
                        "Leaning on ASCII table we find codes of both letters: I = 73; T = 84.\n" +
                        "Now we translate this codes into binary numeral system: 73 = 01001001; 84 = 01010100.\n" +
                        "Now we are just following the 2B1Q rules:\n" +
                        "\tWe start from the bottom left corner and read by front edge. The first two numbers are 01.\n" +
                        "\t\t => we code it as |‾ (1st & 2nd levels). \n" +
                        "\t The next numbers are 00 => we code it as |_ (2nd & 1st levels). \n" +
                        "\t The next numbers are 10 => we code it as |‾  (1st & 4th levels).\n" +
                        "Good job! Continue doing the same job and you will reach the encoding! You can check yourself " +
                        "by encoding word IT (2 key in menu).\n\n";
        }

        /// <summary>
        /// метод, который возвращает граф 2B1Q
        /// </summary>
        /// <param name="code"> длинная строка значений </param>
        /// <returns> Polyline объект с графом 2B1Q </returns>
        public override Polyline GetGraph(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            if (code[0] == '0' && code[1] == '0')
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 100);
                System.Windows.Point point2 = new System.Windows.Point(70, 100);
                points.Add(point1); points.Add(point2);
            }
            else if (code[0] == '0' && code[1] == '1')
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 80);
                System.Windows.Point point2 = new System.Windows.Point(70, 80);
                points.Add(point1); points.Add(point2);
            }
            else if (code[0] == '1' && code[1] == '1')
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 60);
                System.Windows.Point point2 = new System.Windows.Point(70, 60);
                points.Add(point1); points.Add(point2);
            }
            else if (code[0] == '1' && code[1] == '0')
            {
                System.Windows.Point point1 = new System.Windows.Point(50, 40);
                System.Windows.Point point2 = new System.Windows.Point(70, 40);
                points.Add(point1); points.Add(point2);
            }

            for (int i = 4; i < code.Length; i+=2)
            {
                if (code[i - 2] == '0' && code[i - 1] == '0')
                {
                    points.Add(new System.Windows.Point(points[points.Count - 1].X, 100));
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 20, 100));

                }
                else if (code[i - 2] == '0' && code[i - 1] == '1')
                {
                    points.Add(new System.Windows.Point(points[points.Count - 1].X, 80));
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 20, 80));
                }
                else if (code[i - 2] == '1' && code[i - 1] == '1')
                {
                    points.Add(new System.Windows.Point(points[points.Count - 1].X, 60));
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 20, 60));
                }
                else if (code[i - 2] == '1' && code[i - 1] == '0')
                {
                    points.Add(new System.Windows.Point(points[points.Count - 1].X, 40));
                    points.Add(new System.Windows.Point(points[points.Count - 1].X + 20, 40));
                }
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Green;
            polyline.Points = points;

            return polyline;
        }

        public override Polyline GetSyncTact(string code)
        {
            Polyline polyline = new Polyline();
            PointCollection points = new PointCollection();

            System.Windows.Point point1 = new System.Windows.Point(50, 30);
            System.Windows.Point point2 = new System.Windows.Point(50, 10);
            System.Windows.Point point3 = new System.Windows.Point(60, 10);
            System.Windows.Point point4 = new System.Windows.Point(60, 30);
            System.Windows.Point point5 = new System.Windows.Point(70, 30);

            points.Add(point1); points.Add(point2); points.Add(point3); points.Add(point4); points.Add(point5);

            for (int i = 1; i < (code.Length / 2) - 1; i++)
            {
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y - 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
                points.Add(new System.Windows.Point(points[points.Count - 1].X, points[points.Count - 1].Y + 20));
                points.Add(new System.Windows.Point(points[points.Count - 1].X + 10, points[points.Count - 1].Y));
            }

            polyline.StrokeThickness = 3;
            polyline.Stroke = System.Windows.Media.Brushes.Aqua;
            polyline.Points = points;

            return polyline;
        }
    }
}