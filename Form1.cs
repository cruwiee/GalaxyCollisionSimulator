using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection.Emit;
using static CursWork.Form1;



namespace CursWork
{

    public partial class Form1 : Form
    {
        private const double G = 6.67430e-11;
        private const double KPC = 3.0857e19;
        private const double M_bulge = 4.0e40;
        private const double M_sun = 2.0e30;
        const double AU = 1.496e11;

        private Bitmap buffer;
        private Game game;
        private SolidBrush penSun;
        private SolidBrush penStar;

        public class Star
        {
            public double x;
            public double y;
            public double vx;
            public double vy;
            public double ax;
            public double ay;
            public double mass;
            public double vDm;
            public double vKepler;
        }

        private double distance;
        private double darkMatter;
        private Star[] stars;

        private double dt = 360 * 10000000000 / 2;

        public Form1()
        {
            InitializeComponent();

            InitializeGraphics();
            InitializeAnimation();
        }





        private void InitializeGraphics()
        {
            // Инициализация OpenGL
            buffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            penSun = new SolidBrush(Color.LightGoldenrodYellow);
            penStar = new SolidBrush(Color.Blue);

        }

        private void InitializeAnimation()
        {
            stars = new Star[162 * 2];
            stars[80] = new Star();
            stars[161] = new Star();
        }





        private void drawCircle(Graphics g, SolidBrush brush, double x, double y, double r)
        {
            g.FillEllipse(brush, (float)(x - r), (float)(y - r), (float)(2 * r), (float)(2 * r));
        }
   

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            using (Graphics bufferGraphics = Graphics.FromImage(buffer))
            {
               
                bufferGraphics.Clear(Color.Black);

                int count = radioButton2.Checked ? 80 : 40;

                int size = (count + 1) * 3 - 80;

                for (int j = 0; j < count + size; j++)
                {
                    for (int k = 0; k < count; k++)
                    {
                        Star starTo = stars[j];
                        if (starTo == null)
                        {
                            continue;
                        }
                        double axStar = 0;
                        double ayStar = 0;


                        for (int i = 1; i < count + size; i++)
                        {
                            if (i == j) continue;
                            Star starFrom = stars[i];
                            if (starFrom == null)
                            {
                                continue;
                            }
                            double dx = starFrom.x - starTo.x;
                            double dy = starFrom.y - starTo.y;
                            double r2 = dx * dx + dy * dy;
                            double r = Math.Sqrt(r2);
                            double a = G * starFrom.mass / r2 + darkMatter * Math.Sqrt(G * starFrom.mass / r);

                            axStar += a * dx / r;
                            ayStar += a * dy / r;
                        }

                        starTo.x += starTo.vx * dt + 0.5 * starTo.ax * dt * dt;
                        starTo.y += starTo.vy * dt + 0.5 * starTo.ay * dt * dt;

                        starTo.vx += 0.5 * (starTo.ax + axStar) * dt;
                        starTo.vy += 0.5 * (starTo.ay + ayStar) * dt;

                        starTo.ax = axStar;
                        starTo.ay = ayStar;
                    }
                }

                game.setStars(stars);

            }

        }


        private void button1_Click(object sender, EventArgs e)
        {



            stars[80].mass = Convert.ToDouble(bulgeTextBox.Text);
            stars[161].mass = Convert.ToDouble(bulgeTextBox.Text);
            distance = Convert.ToDouble(densityTextBox.Text);
            darkMatter = Convert.ToDouble(darkMatterTextBox.Text);

            CleanAnimation();
            timer1.Enabled = true;
            timer1.Start();

            var gameWindowSettings = new GameWindowSettings();
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(1200, 900),
                Location = new Vector2i(300, 300),
                WindowBorder = WindowBorder.Resizable,
                //WindowState = WindowState.Normal,
                Title = "OpenGL",
                Flags = ContextFlags.Default,
                //APIVersion = new Version(4, 6),
                Profile = ContextProfile.Compatability,
                API = ContextAPI.OpenGL,
            };

            using (game = new Game(gameWindowSettings, nativeWindowSettings))
            {
                game.Run();
            }
        }
        private void CleanAnimation()
        {
            double elliptical = 1;
            
            InitializeAnimation();

           
            int offset = 41;
            double offsetDistance = 15.0 * distance;
            for (int i = 8; i < 8 + offset; i++)
            {
                double alpha = i / 2.0 / double.Pi;
                double rStar = distance * (i / 6.0 + 12);

                double vBase1 = Math.Pow(Math.Pow(G * M_bulge * 2.0 / (rStar), 3.0 / 2.0) + 0.0125 * darkMatter * G * M_bulge, 1.0 / 3.0);
                double vBase2 = Math.Pow(Math.Pow(G * M_bulge * 2.0 / (rStar), 3.0 / 2.0) + 0.0125 * darkMatter * G * M_bulge, 1.0 / 3.0);
                int j = i - 8;
                stars[j] = new Star();
                stars[j + offset] = new Star();
                Star star = stars[j];
                star.mass = (j == 40 ? M_bulge : M_sun);
                star.x = -(rStar) * Math.Sin(alpha) + offsetDistance;
                star.y = -(rStar) * Math.Cos(alpha) + offsetDistance;
                star.vx = vBase1 * Math.Cos(alpha);
                star.vy = -vBase1 * Math.Sin(alpha);
                star = stars[j + offset];
                star.mass = (j + offset == 81 ? M_bulge : M_sun);
                star.x = (rStar) * Math.Sin(alpha) - offsetDistance;
                star.y = (rStar) * Math.Cos(alpha) - offsetDistance;
                star.vx = -vBase2 * Math.Cos(alpha);
                star.vy = vBase2 * Math.Sin(alpha);

                if (radioButton2.Checked)
                {
                    stars[j + offset * 2] = new Star();
                    stars[j + offset * 3] = new Star();
                    star = stars[j + offset * 2];
                    star.mass = M_sun;
                    star.x = (rStar) * Math.Sin(alpha) + offsetDistance;
                    star.y = (rStar) * Math.Cos(alpha) + offsetDistance;
                    star.vx = -vBase1 * Math.Cos(alpha);
                    star.vy = vBase1 * Math.Sin(alpha);
                    star = stars[j + offset * 3];
                    star.mass = M_sun;
                    star.x = -(rStar) * Math.Sin(alpha) - offsetDistance;
                    star.y = -(rStar) * Math.Cos(alpha) - offsetDistance;
                    star.vx = vBase2 * Math.Cos(alpha);
                    star.vy = -vBase2 * Math.Sin(alpha);
                }

            }

            stars[offset - 1].mass = M_bulge / 2.0;
            stars[offset - 1].x = offsetDistance;
            stars[offset - 1].y = offsetDistance;

            stars[offset * 2 - 1].mass = M_bulge / 2.0;
            stars[offset * 2 - 1].x = -offsetDistance;
            stars[offset * 2 - 1].y = -offsetDistance;

            elliptical = 1;

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void помощьF1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = @"Help.docx";
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            System.Diagnostics.ProcessStartInfo startInfo = new ProcessStartInfo(fullPath);
            startInfo.UseShellExecute = true;
            System.Diagnostics.Process.Start(startInfo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

            Microsoft.Office.Interop.Word.Document document = word.Documents.Add();


            Microsoft.Office.Interop.Word.Paragraph distanceTitleParagraph = document.Content.Paragraphs.Add();
            distanceTitleParagraph.Range.Text = "Таблица расстояний";
            distanceTitleParagraph.Range.Font.Bold = 1;
            distanceTitleParagraph.Range.Font.Size = 14;
            distanceTitleParagraph.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

            // Создаем таблицу для расстояний
            Microsoft.Office.Interop.Word.Table distanceTable = document.Tables.Add(distanceTitleParagraph.Range, 21, 2);
            distanceTable.Borders.Enable = 1; // Включаем границы таблицы
            distanceTable.Cell(1, 1).Range.Text = "Расстояние Галактика 1";
            distanceTable.Cell(1, 2).Range.Text = "Расстояние Галактика 2";

           
            distanceTable.PreferredWidthType = Microsoft.Office.Interop.Word.WdPreferredWidthType.wdPreferredWidthPercent;
            distanceTable.PreferredWidth = 80; 
           
            distanceTable.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;

            for (int i = 0; i < 20; i++)
            {
                try
                {
                    distanceTable.Cell(i + 2, 1).Range.Text = stars[i].y.ToString();
                    distanceTable.Cell(i + 2, 2).Range.Text = stars[i + 20].y.ToString();
                }
                catch (COMException ex)
                {
                    // Обработка ошибки
                }
            }

            // Отделяем первую таблицу от второй
            object breakType = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
            distanceTitleParagraph.Range.InsertBreak(ref breakType);



           
            Microsoft.Office.Interop.Word.Table velocityTable = document.Tables.Add(distanceTitleParagraph.Range, 21, 2);
            velocityTable.Borders.Enable = 1; 
            velocityTable.Cell(1, 1).Range.Text = "Скорость Галактика 1";
            velocityTable.Cell(1, 2).Range.Text = "Скорость Галактика 2";

            velocityTable.PreferredWidthType = Microsoft.Office.Interop.Word.WdPreferredWidthType.wdPreferredWidthPercent;
            velocityTable.PreferredWidth = 80; 

            velocityTable.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;

            for (int i = 0; i < 20; i++)
            {
                try
                {
                    velocityTable.Cell(i + 2, 1).Range.Text = stars[i].vx.ToString();
                    velocityTable.Cell(i + 2, 2).Range.Text = stars[i + 20].vx.ToString();
                }
                catch (COMException ex)
                {
                   
                }
            }
            // Определяем путь к файлу и имя файла
            string fileName = @"Отчёт.docx";
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);

            // Сохраняем документ во временном файле
            string tempFilePath = System.IO.Path.GetTempFileName();
            document.SaveAs(tempFilePath);

            // Открываем документ в Word
            word.Visible = true;
            document = word.Documents.Open(tempFilePath);

            // Здесь вы можете внести необходимые изменения в документ вручную

            // Сохраняем изменения
            //document.Save();

            // Закрываем Word и освобождаем ресурсы
            //document.Close();
            //word.Quit();
            //Marshal.ReleaseComObject(document);
            //Marshal.ReleaseComObject(word);
            //GC.Collect();

            //// Сохраняем документ
            //string fileName = @"D:\Users\Кристина\Desktop\Колледж\КПЯП\Курсовой проект\CursWork\Отчёт.docx";
            //document.SaveAs(fileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument);

            //word.Quit();
            //// Release the file lock
            //Marshal.ReleaseComObject(document);
            //Marshal.ReleaseComObject(word);
            //GC.Collect();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            CleanAnimation();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }

}
    
